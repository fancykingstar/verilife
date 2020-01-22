using LSRetail.Omni.Domain.DataModel.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace NAVAccess
{
    public class NavBase
    {
        public NavBase()
        {

        }
        protected string ParseResponseCode(XmlNodeList responseCode)
        {
            XmlNode node = responseCode.Item(0);
            return node.InnerText;
        }
        protected StatusCode MapResponseToStatusCode(string navResponseId, string navCode)
        {
            /*
                MissingStoreNumber = 1001,
                MissingTenderLines = 1002,
             * 
             * */
            //mapping response code from NAV to LSOmni, but sometimes the same navCode is used for different navResponseId
            // so need to check them both - sometimes
            navResponseId = navResponseId.ToUpper().Trim();
            StatusCode statusCode = StatusCode.Error; //default to Error
            switch (navCode)
            {
                case "0000":
                    statusCode = StatusCode.OK;
                    break;
                case "0004":
                    statusCode = StatusCode.NAVWebFunctionNotFound;
                    break;
                case "0030":
                    if (navResponseId == "WEB_POS")
                        statusCode = StatusCode.InvalidNode;
                    break;
                case "0131":
                    statusCode = StatusCode.PasswordInvalid;
                    break;
                case "1001":
                    statusCode = StatusCode.Error;
                    if (navResponseId == "GET_DYN_CONT_HMP_MENUS" || navResponseId == "GET_DYNAMIC_CONTENT")
                        statusCode = StatusCode.NoHMPMenuFound;
                    else if (navResponseId == "MM_LOGIN_CHANGE") // cannot for "MM_CREATE_LOGIN_LINKS")
                        statusCode = StatusCode.UserNameInvalid;
                    else if (navResponseId == "IM_GET_CUSTOMER_CARD_01")
                        statusCode = StatusCode.CustomerNotFound;
                    else if (navResponseId == "IM_GET_ITEM_CARD_01")
                        statusCode = StatusCode.ItemNotFound;
                    else if (navResponseId == "IM_GET_VENDOR_CARD_01")
                        statusCode = StatusCode.VendorNotFound;
                    break;
                case "1002":
                    if (navResponseId == "MM_LOGIN_CHANGE")
                        statusCode = StatusCode.PasswordInvalid;
                    else if (navResponseId == "MM_CREATE_LOGIN_LINKS")
                        statusCode = StatusCode.UserNameNotFound;
                    break;
                case "1003":
                    if (navResponseId == "MM_LOGIN_CHANGE")
                        statusCode = StatusCode.UserNameInvalid;
                    else if (navResponseId == "MM_CREATE_LOGIN_LINKS")
                        statusCode = StatusCode.MemberCardNotFound;
                    break;
                case "1004":
                    if (navResponseId == "MM_LOGIN_CHANGE")
                        statusCode = StatusCode.UserNameExists;
                    break;
                case "1010":
                    if (navResponseId == "LOAD_PUBOFFERS_AND_PERSCOUPONS")
                        statusCode = StatusCode.UserNameExists;
                    break;
                case "1011":
                    if (navResponseId == "LOAD_PUBOFFERS_AND_PERSCOUPONS")
                        statusCode = StatusCode.ItemNotFound;
                    break;
                case "1012":
                    if (navResponseId == "LOAD_PUBOFFERS_AND_PERSCOUPONS")
                        statusCode = StatusCode.StoreNotExists;
                    break;
                case "1013":
                    if (navResponseId == "GET_DYN_CONT_HMP_MENUS" || navResponseId == "GET_DYNAMIC_CONTENT")
                        statusCode = StatusCode.HMPMenuNotEnabled;
                    break;
                case "1014":
                    if (navResponseId == "GET_DYN_CONT_HMP_MENUS" || navResponseId == "GET_DYNAMIC_CONTENT")
                        statusCode = StatusCode.HMPMenuNoDynamicContentFoundToday;
                    break;
                case "1020":
                    if (navResponseId == "LOAD_PUBOFFERS_AND_PERSCOUPONS")
                        statusCode = StatusCode.MemberCardNotFound;
                    break;
                case "1100":
                    statusCode = StatusCode.UserNameExists;
                    if (navResponseId == "MM_CARD_TO_CONTACT")
                        statusCode = StatusCode.AccountNotFound;
                    break;
                case "1101":
                    statusCode = StatusCode.UserNameNotFound;
                    break;
                case "1102":
                    statusCode = StatusCode.ContactIdNotFound;
                    break;
                case "1103":
                    statusCode = StatusCode.ContactIdNotFound;
                    break;
                case "1106":
                    statusCode = StatusCode.QtyMustBePositive;
                    break;
                case "1107":
                    statusCode = StatusCode.LineNoMission;
                    break;
                case "1110":
                    statusCode = StatusCode.UserNameInvalid;
                    break;
                case "1120":
                    statusCode = StatusCode.PasswordInvalid;
                    if (navResponseId == "MM_MOBILE_LOGON")
                        statusCode = StatusCode.AuthFailed;
                    break;
                case "1130":
                    statusCode = StatusCode.EmailInvalid;
                    if (navResponseId == "MM_MOBILE_LOGON")
                        statusCode = StatusCode.CardIdInvalid;
                    break;
                case "1135":
                    statusCode = StatusCode.EmailExists;
                    break;
                case "1140":
                    statusCode = StatusCode.MissingLastName;
                    if (navResponseId == "MM_MOBILE_LOGON")
                        statusCode = StatusCode.LoginIdNotMemberOfClub;
                    break;
                case "1150":
                    statusCode = StatusCode.MissingFirstName;
                    if (navResponseId == "MM_MOBILE_LOGON")
                        statusCode = StatusCode.DeviceIdNotFound;
                    break;
                case "1160":
                    statusCode = StatusCode.AccountNotFound;
                    break;
                case "1170":
                    statusCode = StatusCode.OneAccountInvalid;
                    break;
                case "1180":
                    statusCode = StatusCode.PrivateAccountInvalid;
                    break;
                case "1190":
                    statusCode = StatusCode.ClubInvalid;
                    break;
                case "1200":
                    statusCode = StatusCode.SchemeInvalid;
                    if (navResponseId == "MM_CARD_TO_CONTACT")
                        statusCode = StatusCode.ContactIdNotFound;
                    break;
                case "1210":
                    statusCode = StatusCode.ClubOrSchemeInvalid;
                    break;
                case "1212":
                    statusCode = StatusCode.SchemeClubInvalid;
                    break;
                case "1220":
                    statusCode = StatusCode.AccountContactIdInvalid;
                    break;
                case "1230":
                    statusCode = StatusCode.AccountExistsInOtherClub;
                    break;

                case "1300":
                    statusCode = StatusCode.PasswordOldInvalid;
                    if (navResponseId == "MM_CARD_TO_CONTACT")
                        statusCode = StatusCode.ContactIdNotFound;
                    break;
                case "1310":
                    statusCode = StatusCode.PasswordInvalid; //invalid new password
                    break;
                case "1400":
                    statusCode = StatusCode.MissingItemNumer;
                    if (navResponseId == "MM_CARD_TO_CONTACT")
                        statusCode = StatusCode.AccountNotFound;
                    break;
                case "1410":
                    statusCode = StatusCode.MissingStoreNumber;
                    break;
                case "1450":
                    statusCode = StatusCode.DeviceIdMissing;
                    break;
                case "1500":
                    if (navResponseId == "MM_CARD_TO_CONTACT")
                        statusCode = StatusCode.ContactIdNotFound;
                    break;
                case "1600":
                    statusCode = StatusCode.PosNotExists;
                    if (navResponseId == "MM_CARD_TO_CONTACT")
                        statusCode = StatusCode.MemberCardNotFound;
                    break;
                case "1601":
                    statusCode = StatusCode.StoreNotExists;
                    break;
                case "1602":
                    statusCode = StatusCode.StaffNotExists;
                    break;
                case "1603":
                    statusCode = StatusCode.ItemNotExists;
                    break;
                case "1604":
                    statusCode = StatusCode.VATSetupMissing;
                    break;
                case "1605":
                    statusCode = StatusCode.InvalidUom;
                    break;
                case "1606":
                    statusCode = StatusCode.ItemBlocked;
                    break;
                case "1607":
                    statusCode = StatusCode.InvalidVariant;
                    break;
                case "1608":
                    statusCode = StatusCode.InvalidPriceChange;
                    break;
                case "1609":
                    statusCode = StatusCode.PriceChangeNotAllowed;
                    break;
                case "1610":
                    statusCode = StatusCode.PriceToHigh;
                    break;
                case "1611":
                    statusCode = StatusCode.InvalidDiscPercent;
                    break;
                case "1612":
                    statusCode = StatusCode.IncExpNotFound;
                    break;
                case "1613":
                    statusCode = StatusCode.TenderTypeNotFound;
                    break;
                case "1614":
                    statusCode = StatusCode.InvalidTOTDiscount;
                    break;
                case "1615":
                    statusCode = StatusCode.NotMobilePos;
                    break;
                case "1619":
                    statusCode = StatusCode.InvalidPostingBalance;
                    break;
                case "1620":
                    statusCode = StatusCode.SuspendWithPayment;
                    if (navResponseId == "WEB_POS")
                        statusCode = StatusCode.PaymentPointsMissing; //Payment with member points, %1 missing.
                    break;
                case "1621":
                    statusCode = StatusCode.UnknownSuspError;
                    if (navResponseId == "WEB_POS")
                        statusCode = StatusCode.MemberCardNotFound; //Unable to load member information – ErrorText from LOAD_MEMBER _INFO displayed
                    break;
                case "1625":
                    statusCode = StatusCode.SuspKeyNotFound;
                    break;
                case "1626":
                    statusCode = StatusCode.TransServError;
                    break;
                case "1627":
                    statusCode = StatusCode.SuspTransNotFound;
                    break;
                case "1670":
                    statusCode = StatusCode.CustomerNotFound;
                    break;
                case "1673":
                    statusCode = StatusCode.NoEntriesFound;
                    break;
                case "1674":
                    statusCode = StatusCode.MemberAccountNotFound;
                    break;
                case "1675":
                    statusCode = StatusCode.MemberCardNotFound;
                    break;
                case "1676":
                    statusCode = StatusCode.NoEntriesFound2;
                    break;
                case "1677":
                    statusCode = StatusCode.NoEntriesFound3;
                    break;
                case "1678":
                    statusCode = StatusCode.NoEntriesFound4;
                    break;

                case "1698":
                    statusCode = StatusCode.InvalidPrinterId;
                    break;
                case "1700":
                    if (navResponseId == "MM_CARD_TO_CONTACT")
                        statusCode = StatusCode.CardInvalidInUse;
                    break;
                case "1800":
                    statusCode = StatusCode.TerminalIdMissing;
                    if (navResponseId == "MM_CARD_TO_CONTACT")
                        statusCode = StatusCode.ClubInvalid;
                    break;
                case "1801":
                    statusCode = StatusCode.TransacitionIdMissing;
                    break;
                case "1802":
                    statusCode = StatusCode.EmailMissing;
                    break;
                case "1900":
                    if (navResponseId == "MM_CARD_TO_CONTACT")
                        statusCode = StatusCode.CardInvalidStatus;
                    break;
                case "2000":
                    statusCode = StatusCode.InvalidPrintMethod;
                    if (navResponseId == "MM_CARD_TO_CONTACT")
                        statusCode = StatusCode.ContactIsBlocked;
                    break;
                case "0403":
                    statusCode = StatusCode.ServerRefusingToRespond;
                    break;
                case "4003":
                    statusCode = StatusCode.DiningTableStatusNotAbleToChange;
                    break;
                case "4004":
                    statusCode = StatusCode.CannotChangeNumberOfCoverOnTableNotSeated;
                    break;
                case "4005":
                    statusCode = StatusCode.CannotChangeNumberOfCoverOnTableNoSetup;
                    break;
                case "4006":
                    statusCode = StatusCode.SeatingNotUsedInHospType;
                    break;
                case "4007":
                    statusCode = StatusCode.StatusOfTableAlredySeated;
                    break;
                case "4008":
                    statusCode = StatusCode.SeatingNotPossible;
                    break;
                case "4009":
                    statusCode = StatusCode.POSTransNotFoundForActiveOrder;
                    break;
                case "4010":
                    statusCode = StatusCode.NoKitchenStatusFound;
                    break;
                case "4011":
                    statusCode = StatusCode.OpenPOSNotALlowed;
                    break;
                case "4020":
                    statusCode = StatusCode.MainStatusNorCorrect;
                    break;
                case "4021":
                    statusCode = StatusCode.TableAlreadyLocked;
                    break;
                case "7750":  //Percassi only
                    statusCode = StatusCode.SuspendFailure;
                    break;

                default:
                    break;
            }
            return statusCode;
        }
        protected StatusCode GetStatusCode(ref string xmlResponse)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlResponse);
            string navResponseCode = ParseResponseCode(doc.GetElementsByTagName("Response_Code"));
            string navResponseId = ParseResponseCode(doc.GetElementsByTagName("Request_ID"));
            return MapResponseToStatusCode(navResponseId, navResponseCode);
        }
        protected string HandleResponseCode(ref string xmlResponse)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlResponse);
            string responseCode = doc.GetElementsByTagName("Response_Code").Item(0).InnerText;
            if (responseCode != "0000")
            {
                string navResponseId = doc.GetElementsByTagName("Request_ID").Item(0).InnerText;
                string navResponseText = doc.GetElementsByTagName("Response_Text").Item(0).InnerText;
                string msg = string.Format("navResponseCode: {0} - {1} ", responseCode, navResponseText);
                //expected return codes, so dont throw unexpected exception, rather return the known codes to client  
                throw new Exception(msg);
            }
            return null;
        }
        protected string GetResponseCode(ref string xmlResponse)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlResponse);
            string navResponseCode = ParseResponseCode(doc.GetElementsByTagName("Response_Code"));
            return navResponseCode;
        }
    }

}