using LSOmni.DataAccess.BOConnection.NavSQL.XmlMapping.Loyalty;
using LSRetail.Omni.Domain.DataModel.Base;
using LSRetail.Omni.Domain.DataModel.Base.Retail;
using LSRetail.Omni.Domain.DataModel.Loyalty.Items;
using LSRetail.Omni.Domain.DataModel.Loyalty.Members;
using LSRetail.Omni.Domain.DataModel.Loyalty.Setup;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Xml;

namespace NAVAccess
{
    public interface IRetailWebservice
    {
        // MemberContact ContactGetById(string ContactID);
        List<PublishedOffer> PublishedOffersGet(string cardId, string itemId, string storeId);
        List<LoyItem> PublishedOfferItemsGet(string offerId);
        ContactRs Login(string userName, string password, string cardId);
        ContactRs ContactCreate(MemberContact contact);
        MemberContact ContactGetByCardId(string CardID);
        void ContactUpdate(MemberContact contact, string accountId);
        List<Notification> NotificationsGetByContactId(string cardId, int numberOfNotifications);
        void SetURL(string Url);


    }
    public class RetailWebservice : NavBase, IRetailWebservice
    {
        string CODEUNIT = "RetailWebServices";

        BasicHttpBinding _basicHttpBinding = null;
        EndpointAddress _endpointAddress = null;
        ChannelFactory<RetailWebServices_PortChannel> _factory = null;
        RetailWebServices_PortChannel _serviceProxy = null;
        string _user = null;
        string _password = null;

        public void SetURL(string Url)
        {
            _endpointAddress = new EndpointAddress(new Uri(Url + CODEUNIT));
            _factory = new ChannelFactory<RetailWebServices_PortChannel>(_basicHttpBinding, _endpointAddress);
            _factory.Credentials.UserName.UserName = _user;
            _factory.Credentials.UserName.Password = _password;

        }

        public RetailWebservice(string url, string user, string password, string company)
        {
            var finalurl = url.Replace("{company}", company);
            _basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            _basicHttpBinding.MaxReceivedMessageSize = Int32.MaxValue;
            _basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            _endpointAddress = new EndpointAddress(new Uri(finalurl + CODEUNIT));
            _factory = new ChannelFactory<RetailWebServices_PortChannel>(_basicHttpBinding, _endpointAddress);
            _factory.Credentials.UserName.UserName = user;
            _factory.Credentials.UserName.Password = password;
            _user = user;
            _password = password;
        }

        #region Base
        string RunOperation(string RequestXML, bool CloseCommunication = true)
        {
            string xmlResponse = "";
            try
            {
                _serviceProxy = _factory.CreateChannel();

                using (var scope = new OperationContextScope((IContextChannel)_serviceProxy))
                {
                    WebRequestBody body = new WebRequestBody(RequestXML, xmlResponse);
                    WebRequest wr = new WebRequest(body);
                    var ret = _serviceProxy.WebRequestAsync(wr).GetAwaiter().GetResult();
                    xmlResponse = ret.Body.pxmlResponse;
                    HandleResponseCode(ref xmlResponse);
                }
                if (CloseCommunication == true)
                {
                    _factory.Close();
                    ((ICommunicationObject)_serviceProxy).Close();
                }
            }
            catch (Exception ex)
            {
                if (CloseCommunication == false)
                {
                    CloseCommunicationObjects((ICommunicationObject)_serviceProxy, _factory);
                }
                throw;
            }
            finally
            {
                if (CloseCommunication == true)
                {
                    CloseCommunicationObjects((ICommunicationObject)_serviceProxy, _factory);
                }
            }
            return xmlResponse;
        }
    
        private void CloseCommunicationObjects(params ICommunicationObject[] objects)
        {
            foreach (ICommunicationObject comObj in objects)
            {
                try
                {
                    if (comObj == null)
                    {
                        continue;
                    }
                    // Only want to call Close if it is in the Opened state
                    if (comObj.State == CommunicationState.Opened)
                    {
                        comObj.Close();
                    }
                    // Anything not closed by this point should be aborted
                    if (comObj.State != CommunicationState.Closed)
                    {
                        comObj.Abort();
                    }
                }
                catch (TimeoutException)
                {
                    comObj.Abort();
                }
                catch (CommunicationException)
                {
                    comObj.Abort();
                }
            }
        }

        #endregion

        public List<Notification> NotificationsGetByContactId(string cardId, int numberOfNotifications)
        {
            List<Notification> pol = new List<Notification>();
            PublishedOfferXml xml = new PublishedOfferXml();

            string xmlRequest = xml.PublishedOfferMemberRequestXML(cardId, string.Empty, string.Empty);
            string xmlResponse = RunOperation(xmlRequest);
            HandleResponseCode(ref xmlResponse);
            return xml.NotificationMemberResponseXML(xmlResponse);
        }

        #region Offers
        public List<PublishedOffer> PublishedOffersGet(string cardId, string itemId, string storeId)
        {
            PublishedOfferXml xml = new PublishedOfferXml();
            string xmlRequest = xml.PublishedOfferRequestXML(cardId, itemId, storeId);
            string xmlResponse = RunOperation(xmlRequest);
            var ret = xml.PublishedOfferResponseXML(xmlResponse);
            return ret;
        }
        public List<LoyItem> PublishedOfferItemsGet(string offerId)
        {
            PublishedOfferXml xml = new PublishedOfferXml();
            string xmlRequest = xml.PublishedOfferItemsRequestXML(offerId, 50);
            string xmlResponse = RunOperation(xmlRequest);
            var ret = xml.PublishedOfferItemsResponseXML(xmlResponse);
            return ret;

        }
        #endregion

        #region Contact
        public ContactRs Login(string userName, string password, string cardId)
        {
            //first login is to see if login buffer has more than 2, if so need to login again with correct CardId that was picked.
            ContactRs contactRs = Logon(userName, password, cardId);
            if (string.IsNullOrWhiteSpace(contactRs.ContactId))
                contactRs = Logon(userName, password, contactRs.CardId);
            return contactRs;
        }

        private ContactRs Logon(string userName, string password, string cardId)
        {
            NavXml navXml = new NavXml();
            string xmlRequest = navXml.LoginRequestXML(userName, password, cardId);
            string xmlResponse = RunOperation(xmlRequest);
            HandleResponseCode(ref xmlResponse);
            ContactRs contactRs = navXml.LoginResponseXML(xmlResponse);
            return contactRs;
        }

        public ContactRs ContactCreate(MemberContact contact)
        {
            if (contact == null)
                throw new ApplicationException("Contact can not be null");

            //must have a deviceId, otherwise no "Member Login Card" entry is made in nav
            /* if (contact.LoggedOnToDevice == null)
             {
                 contact.LoggedOnToDevice = new Device(GetDefaultDeviceId(contact.UserName));
             }
             if (string.IsNullOrWhiteSpace(contact.LoggedOnToDevice.Id))
             {
                 contact.LoggedOnToDevice.Id = GetDefaultDeviceId(contact.UserName);
             }
             if (contact.Profiles == null)
             {
                 contact.Profiles = new List<Profile>();
             }
             if (string.IsNullOrWhiteSpace(contact.LoggedOnToDevice.DeviceFriendlyName))
                 contact.LoggedOnToDevice.DeviceFriendlyName = "Web application";
            */
            // in invicta denis hardcoded adding member card automatically with the same account ID
            // in 99009511 codeunit line 6888 if device id not empty it create another one member card
            // so we send empty device to avid it, 
            // because pos do not understand 2 member cards when select customer
            contact.LoggedOnToDevice = new Device()
            {
                Id = "",
                DeviceFriendlyName = ""
            };
            NavXml navXml = new NavXml();
            string xmlRequest = navXml.ContactCreateRequestXML(contact);
            string xmlResponse = RunOperation(xmlRequest);
            HandleResponseCode(ref xmlResponse);
            ContactRs contactRs = navXml.ContactCreateResponseXML(xmlResponse);

            //CALL NAV web service and send in the cardId so it will link the card to user 
            //and we only want to link the card to the user, not to the device
            // CreateDeviceAndLinkToUser(contact.UserName, "", "", contactRs.CardId); -- WHAI IS IT????

            return contactRs;
        }
        public string ResetPassword(string userName, string newPassword)
        {
            //Reset the password in NAV
            //used when a user forgot his pwd, and has been validated (URL sent to him)

            //If newPassword is empty then NAV creates a random password ?
            //return the Encrypted pwd that NAV returned to us
            NavXml navXml = new NavXml();
            string xmlRequest = navXml.ResetPasswordRequestXML(userName, newPassword);
            string xmlResponse = RunOperation(xmlRequest);
            HandleResponseCode(ref xmlResponse);
            return string.Empty;
        }

        public void ContactUpdate(MemberContact contact, string accountId)
        {
            NavXml navXml = new NavXml();
            if (contact == null)
                throw new ApplicationException("ContactRq can not be null");

            if (contact.Profiles == null)
            {
                contact.Profiles = new List<Profile>();
            }

            string xmlRequest = navXml.ContactUpdateRequestXML(contact, accountId);
            string xmlResponse = RunOperation(xmlRequest);
            HandleResponseCode(ref xmlResponse);
            return;

        }
        #endregion

        #region Device
        private string GetDefaultDeviceId(string userName)
        {
            return ("WEB-" + userName);
        }

        /*public void CreateDeviceAndLinkToUser(string userName, string deviceId, string deviceFriendlyName, string cardId = "")
        {
            NavXml navXml = new NavXml();
            string xmlRequest = navXml.CreateDeviceAndLinkToUser(userName, deviceId, deviceFriendlyName, cardId);
            string xmlResponse = RunOperation(xmlRequest);
            StatusCode statusCode = GetStatusCode(ref xmlResponse);
            if (statusCode == StatusCode.Error)
            {
                string navResponseCode = GetResponseCode(ref xmlResponse);
                //1001 = Member Login Device already exist,  NavResponseCode.Error = 1001
                // logger.Info("responseCode {0} ignored for userName {1}  deviceId {2}", navResponseCode, userName, deviceId);
                return;
            }
            else if (statusCode != StatusCode.OK)
            {
                switch (statusCode)
                {
                    case StatusCode.DeviceIdMissing: //MissingDeviceId:
                    case StatusCode.UserNotLoggedIn: //LoginIdNotFound:
                        return;
                }
            }
            HandleResponseCode(ref xmlResponse);
        }*/
        #endregion

        public MemberContact ContactGetByCardId(string CardID)
        {
            ContactXml xml = new ContactXml();
            // 1073790705
            string xmlRequest = xml.RequestXML(CardID, 100);
            string xmlResponse = RunOperation(xmlRequest);
            HandleResponseCode(ref xmlResponse);
            MemberContact returnContact = xml.ResponseXML(xmlResponse);
            return returnContact;
        }
    }
}
