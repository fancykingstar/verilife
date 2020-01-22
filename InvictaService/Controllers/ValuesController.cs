using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DBAccess;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using LSRetail.Omni.Domain.DataModel.Loyalty.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NAVAccess;

namespace InvictaService.Controllers
{
    [ApiController]
    public class ValuesController : Controller
    {
        private IRetailWebservice _retailWebService;
        private IJSONWebService _JSONWebService;
        private IConfiguration _configuration;


        public ValuesController(IRetailWebservice retailWebService, IJSONWebService JSONWebService, IConfiguration Configuration)
        {
            _retailWebService = retailWebService;
            _JSONWebService = JSONWebService;

            _configuration = Configuration;
            throw new Exception("FIAL");
     

        }

        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Test")]
        public async Task<string> Get()
        {
            /*var app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("invicta-loyalty-firebase.json")
            });

            var messaging = FirebaseMessaging.GetMessaging(app);
            var msg = new Message()
            {
                Topic = "/topics/all",
                Notification = new Notification()
                {
                    Body = "111111",
                    Title = "22222"
                }
            };

            var ret = await messaging.SendAsync(msg);
            var ret2 = FirebaseMessaging.DefaultInstance.SendAsync(msg).GetAwaiter();*/
            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Push")]
        public async Task<string> Push(string title, string body)
        {
            var msg = new Message()
            {
                Topic = "/topics/all",
                Notification = new Notification()
                {
                    Body = body,
                    Title = title
                }
            };
            var ret = await FirebaseMessaging.DefaultInstance.SendAsync(msg);
            return ret;
        }
    }
}
