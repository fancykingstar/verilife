using System;
using System.Collections.Generic;
using DBAccess;
using LSRetail.Omni.Domain.DataModel.Base.Retail;
using LSRetail.Omni.Domain.DataModel.Loyalty.Items;
using LSRetail.Omni.Domain.DataModel.Loyalty.Members;
using LSRetail.Omni.Domain.DataModel.Loyalty.Setup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NAVAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;

namespace InvictaService.Controllers
{
    public class ContactController : Controller
    {
        private IJSONWebService _JSONWebService;
        private IConfiguration _configuration;
        private IRetailWebservice _retailWebService;
        private IMemoryCache _cache;
        private IHttpContextAccessor _accessor;
        public class JsonData
        {
            public string json { get; set; }
        }

        public ContactController(IJSONWebService JSONWebService, IHttpContextAccessor accessor,
            IConfiguration Configuration, IRetailWebservice retailWebService, IMemoryCache memoryCache)
        {
            _JSONWebService = JSONWebService;
            _configuration = Configuration;
            _retailWebService = retailWebService;
            _cache = memoryCache;
            _accessor = accessor;
        }
        private void SetServicesUrlCompany(string Company = "Retailing Enterprises, LLC")
        {
            string navServiceUrl = _configuration["Nav:ServiceUrl"];
            var url = navServiceUrl.Replace("{company}", Company);
             _JSONWebService.SetURL(url);
            _retailWebService.SetURL(url);
        }
        private string BuildToken(string AccountID)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "Customer"),
                new Claim(ClaimTypes.NameIdentifier, AccountID, ClaimValueTypes.String)
        };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(2628000),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private void CheckAccess(string accountID)
        {
            /*string id = User.Identity.GetUserId<string>();
            if (accountID != id)
            {
                throw new Exception("Unauthorized - Invalid user ID");
            }*/
        }
        private void SetAttemptNumber(string IP, int Number)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
           // Keep in cache for this time, reset time if accessed.
           .SetSlidingExpiration(TimeSpan.FromMinutes(10));
            _cache.Set(IP, Number, cacheEntryOptions);
        }
        private int GetAttemptNumber(string IP)
        {
            int attemptNumber = 0;
            _cache.TryGetValue(IP, out attemptNumber);
            return attemptNumber;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/ContactGetByCardId")]
        public MemberContact ContactGetByCardId(string CardID)
        {
            CheckAccess(CardID);
             var ret = _retailWebService.ContactGetByCardId(CardID);
            ret.BirthDay = ret.BirthDay.AddDays(1);
            ret.ExternalInfo = _JSONWebService.GetJSON("GetContactExtInfo", "{accountNo: '"+ CardID + "'}");
            return ret;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/ContactGetByCardIdV2")]
        public MemberContact ContactGetByCardIdV2(string CardID)
        {
            CheckAccess(CardID);
            var ret = _retailWebService.ContactGetByCardId("C000000004");
            ret.ExternalInfo = _JSONWebService.GetJSON("GetContactExtInfo", "{accountNo: '" + "MA00000001" + "'}");
            return ret;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/PublishedOffersGet")]
        public List<PublishedOffer> PublishedOffersGet(string CardID, string Company)
        {
            // CheckAccess(CardID);
            // SetServicesUrlCompany(Company);
            var ret = _retailWebService.PublishedOffersGet("C000000004", "", "");
            return ret;
        }


        [HttpPost]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/ContactUpdate")]
        public string ContactUpdate([FromBody]MemberContact MemberContact)
        {
            // CheckAccess(MemberContact.Id);
            string regInfo = "{'id': '" + MemberContact.Id + "', 'email' : '" + MemberContact.Email + "'}";
            string ret = _JSONWebService.GetJSON("LyaltyEmailUpdate", regInfo);
            if (ret != "success")
            {
                return ret;
            }
            _retailWebService.ContactUpdate(MemberContact, "MA00000001");
            return "success";
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/[controller]/ContactCheckByEmail")]
        public string ContactCheckByEmail(string Email)
        {
            var ret = _JSONWebService.GetJSON("ContactCheckByEmail", Email);
            if (ret == "Not exist")
            {
                return ret;
            }
            else
            {
                MailService ms = new MailService(_configuration);
                var dir = _configuration["MailTemplate:dir"];
                string template = System.IO.File.ReadAllText(dir + "\\sendcode.html");
                string replaced = template.Replace("{code}", ret);
                ms.SendEmail(Email, "Loyalty Registration Code", replaced);
                return "Sent";
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/[controller]/GetContactByExternalID")]
        public string GetContactByExternalID(string ExternalID)
        {
            string IP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            int ipAttemntNumber = GetAttemptNumber(IP);
            ipAttemntNumber++;
            SetAttemptNumber(IP, ipAttemntNumber);
            if (ipAttemntNumber > 5)
            {
                return "Blocked";
            }

            var ret = _JSONWebService.GetJSON("GetContactByExternalID", ExternalID);
            return ret;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/ContactSetAttributes")]
        public string ContactSetAttributes([FromBody]JsonData jsonData)
        {
            var ret = _JSONWebService.GetJSON("ContactSetAttributes", jsonData.json);
            return ret;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/ContactGetAttributes")]
        public string ContactGetAttributes(string AccountID)
        {
            CheckAccess(AccountID);
            var ret = _JSONWebService.GetJSON("ContactGetAttributes", AccountID);
            return ret;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/ContactChangePassword")]
        public string ContactChangePassword(string AccountID, string Old, string New)
        {
            CheckAccess(AccountID);
            string data = "{'id': '" + AccountID + "', 'old' : '" + Old + "' , 'new': '" + New + "'}";
            var ret = _JSONWebService.GetJSON("ContactChangePassword", data);
            return ret;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/[controller]/ContactRegister")]
        public IActionResult ContactRegister(string RegInfo)
        {
            IActionResult response = null;

            var ret = _JSONWebService.GetJSON("ContactRegister", RegInfo);
            if (ret.IndexOf("error") != -1)
            {
                response = Ok(new
                {
                    error = ret
                });
                return response;
            }
            var tokenString = BuildToken(ret);
            response = Ok(new
            {
                token = tokenString
            });
            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/[controller]/ContactRegisterByEmail")]
        public IActionResult ContactRegisterByEmail(string Name, string Email, string Password)
        {
            IActionResult response = null;
            string IP = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            int ipAttemntNumber = GetAttemptNumber(IP);
            ipAttemntNumber++;
            SetAttemptNumber(IP, ipAttemntNumber);
            if (ipAttemntNumber > 5)
            {
                response = Ok(new
                {
                    error = "error. too many attments, ip blocked for 10 minutes."
                });
                return response;
            }

            MemberContact contact = new MemberContact()
            {
                UserName = Email,
                Name = Name,
                Email = Email,
                Password = Password
            };
            var contactRs = _retailWebService.ContactCreate(contact);
            string regInfo = "{'id': '"+ contactRs.AccountId + "', 'email' : '" + Email + "' , 'pass': '" + Password + "'}";
            string ret = _JSONWebService.GetJSON("ContactRegister", regInfo);
            if (ret.IndexOf("error") != -1)
            {

                response = Ok(new
                {
                    error = "error. " + ret
                });
                return response;
            }
            var tokenString = BuildToken(contactRs.AccountId);
            response = Ok(new
            {
                token = tokenString,
                accountID = contactRs.AccountId
            });

            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/[controller]/ContactLogin")]
        public IActionResult ContactLoign(string LoginInfo)
        {
            IActionResult response = Unauthorized();

            var accountID = _JSONWebService.GetJSON("ContactLogin", LoginInfo);
            if (accountID != "error")
            {
                var tokenString = BuildToken(accountID);
                response = Ok(new
                {
                    token = tokenString,
                    accountID= accountID.Replace("MA", "MC")
                });
            }
            return response;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/[controller]/ResetPassword")]
        public string ResetPassword(string Email)
        {
            var ret = _JSONWebService.GetJSON("ResetPassword", Email);
            if (ret != "error")
            {
                MailService ms = new MailService(_configuration);
                var dir = _configuration["MailTemplate:dir"];
                string template = System.IO.File.ReadAllText(dir + "\\reset.html");
                string link = "http://invictaloyalty.com//#/" + ret;
                string replaced = template.Replace("{link}", link);
                ms.SendEmail(Email, "Password Reset", replaced);
            }
            return ret;
        }
        /*
        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/NotificationsGetByContactId")]
        public List<Notification> NotificationsGetByContactId(string ContactID)
        {
            CheckAccess(ContactID);
            var ret = _dbRepository.NotificationsGetByContactId(ContactID);
            return ret;
        }
        */
        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/NotificationsGetByContactId")]
        public string NotificationsGetByContactId(string ContactID)
        {
            //CheckAccess(ContactID);
            var ret = _JSONWebService.GetJSON("GetMemberNotifications", "MA00000001");
            return ret;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/GetRefundList")]
        public string GetRefundList(string ContactID)
        {
            CheckAccess(ContactID);
            var ret = _JSONWebService.GetJSON("GetRefundList", ContactID);
            return ret;
        }
        #region Items

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/GetPublishedItems")]
        public string GetPublishedItems()
        {
            var ret = _JSONWebService.GetJSON("GetPublishedItems", "");
            return ret;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/GetItemDetails")]
        public string GetItemDetails(string ItemNo)
        {
            var ret = _JSONWebService.GetJSON("GetItemDetails", ItemNo);
            return ret;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("api/[controller]/GetItemsPerGroup")]
        public string GetItemsPerGroup(string GroupCode, string Page)
        {
            string req = "{'groupCode': '" + GroupCode + "', 'page' : '" + Page + "'}";
            var ret = _JSONWebService.GetJSON("GetItemsPerGroup", req);
            return ret;
        }
        #endregion

        [HttpGet]
        [Route("api/[controller]/GetDigitalReceipt")]
        public IActionResult GetDigitalReceipt(string StoreNo, string PosTerminalNo, string TransactionNo, string Company = "Retailing Enterprises, LLC")
        {
            SetServicesUrlCompany(Company);

            var param = "{StoreNo : '" + StoreNo + "', PosTerminalNo : '" + PosTerminalNo +
                "', TransactionNo : '" + TransactionNo + "', PrintMethod : 'GetPDF', MailRecipient : ''}";
            var ret = _JSONWebService.GetJSON("GetDigitalReceipt", param);
            var bytes = Convert.FromBase64String(ret);
            return File(bytes, "application/pdf", "Den-Receipt-" + TransactionNo + ".pdf");

        }
        [HttpGet]
        [Route("api/[controller]/GetDigitalReceiptJSON")]
        public string GetDigitalReceiptJSON(string StoreNo, string PosTerminalNo, string TransactionNo, string Company = "Retailing Enterprises, LLC")
        {
            SetServicesUrlCompany(Company);

            var param = "{StoreNo : '" + StoreNo + "', PosTerminalNo : '" + PosTerminalNo +
                "', TransactionNo : '" + TransactionNo + "', PrintMethod : 'JSON', MailRecipient : ''}";
            var ret = _JSONWebService.GetJSON("GetDigitalReceipt", param);
            return ret;

        }

        [HttpGet]
        [Route("api/[controller]/GetFeaturedWatch")]
        public string GetFeaturedWatch()
        {
            var ret = _JSONWebService.GetJSON("GetFeaturedWatch", "");
            return ret;

        }

        [HttpGet]
        [Route("api/[controller]/GetAddressByZip")]
        [AllowAnonymous]
        public string GetFeaturedWatch(string Code)
        {
            var ret = _JSONWebService.GetJSON("GetAddressByZip", Code);
            return ret;

        }

        [HttpGet]
        [Route("api/[controller]/GetOfferHtml")]
        [AllowAnonymous]
        public string GetOfferHtml(string Code)
        {
            var ret = _JSONWebService.GetJSON("GetOfferHtml", Code);
            return ret;

        }
    }
}
