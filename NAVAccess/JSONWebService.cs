using DenService;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Xml;

namespace NAVAccess
{

    public interface IJSONWebService
    {
        string GetJSON(string requestName, string requestParams);
        void SetURL(string Url);
    }
    public class JSONWebService : IJSONWebService
    {
        string CODEUNIT = "LoyaltyJsonService";

        BasicHttpBinding _basicHttpBinding = null;
        EndpointAddress _endpointAddress = null;
        ChannelFactory<LoyaltyJsonService_PortChannel> _factory = null;
        LoyaltyJsonService_PortChannel _serviceProxy = null;
        string _user = null;
        string _password = null;
        public void SetURL(string Url)
        {
            _endpointAddress = new EndpointAddress(new Uri(Url + CODEUNIT));
            _factory = new ChannelFactory<LoyaltyJsonService_PortChannel>(_basicHttpBinding, _endpointAddress);
            _factory.Credentials.UserName.UserName = _user;
            _factory.Credentials.UserName.Password = _password;
        }
        public JSONWebService(string url, string user, string password, string company)
        {
            var finalUrl = url.Replace("{company}", company);
            _basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            _basicHttpBinding.MaxReceivedMessageSize = Int32.MaxValue;
            _basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            _endpointAddress = new EndpointAddress(new Uri(finalUrl + CODEUNIT));
            _factory = new ChannelFactory<LoyaltyJsonService_PortChannel>(_basicHttpBinding, _endpointAddress);
            _factory.Credentials.UserName.UserName = user;
            _factory.Credentials.UserName.Password = password;
            _user = user;
            _password = password;
        }

        public string GetJSON(string requestName, string requestParams)
        {
            string returnString = null;
            try
            {
                _serviceProxy = _factory.CreateChannel();

                using (var scope = new OperationContextScope((IContextChannel)_serviceProxy))
                {
                    GetJSON param = new GetJSON(requestName, requestParams);
                    var ret = _serviceProxy.GetJSONAsync(param).GetAwaiter().GetResult();
                    returnString = ret.return_value;
                }
                _factory.Close();
                ((ICommunicationObject)_serviceProxy).Close();
            }
            catch (MessageSecurityException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseCommunicationObjects((ICommunicationObject)_serviceProxy, _factory);
            }
            return returnString;
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
    }
}
