using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NAVAccess;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace InvictaService.Controllers
{
    public class ImagesController : Controller
    {
        private IJSONWebService _JSONWebService;
        private IConfiguration _configuration;

        public ImagesController(IJSONWebService JSONWebService, IConfiguration Configuration)
        {
            _JSONWebService = JSONWebService;
            _configuration = Configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/[controller]/Remove/{Code}")]
        public string RemoveImage(string Code)
        {
            var dir = _configuration["Img:dir"];
            try
            {
                System.IO.File.Delete(dir + "\\" + Code);
                return "success";

            }
            catch (System.IO.FileNotFoundException e)
            {
                return "error " + e.Message;
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("api/[controller]/{Code}")]
        public IActionResult Image(string Code)
        {
            byte[] array = null;
            var dir = _configuration["Img:dir"];
            try
            {
                array = System.IO.File.ReadAllBytes(dir + "\\" + Code);
                var format = SixLabors.ImageSharp.Image.DetectFormat(array);
                return File(array, format.MimeTypes.First());
            }
            catch (System.IO.FileNotFoundException)
            {
                var base64String = _JSONWebService.GetJSON("GetRetailImage", Code);
                var bytes = Convert.FromBase64String(base64String);
                System.IO.File.WriteAllBytes(dir + "\\" + Code, bytes);
                var format = SixLabors.ImageSharp.Image.DetectFormat(bytes);
                return File(bytes, format.MimeTypes.First());
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("api/[controller]/Item/{Code}")]
        public IActionResult ItemImage(string Code)
        {
            byte[] array = null;
            var dir = _configuration["Img:dir"];
            try
            {
                array = System.IO.File.ReadAllBytes(dir + "\\" + Code);
                var format = SixLabors.ImageSharp.Image.DetectFormat(array);
                return File(array, format.MimeTypes.First());
            }
            catch (System.IO.FileNotFoundException)
            {
                var base64String = _JSONWebService.GetJSON("GetItemImage", Code);
                if (base64String == "")
                {
                    array = System.IO.File.ReadAllBytes(dir + "\\EMPTY");
                    var eformat = SixLabors.ImageSharp.Image.DetectFormat(array);
                    return File(array, eformat.MimeTypes.First());
                }
                var bytes = Convert.FromBase64String(base64String);
                System.IO.File.WriteAllBytes(dir + "\\" + Code, bytes);
                var format = SixLabors.ImageSharp.Image.DetectFormat(bytes);
                return File(bytes, format.MimeTypes.First());
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Resize")]
        public string Resize(string url = "https://cdn.invictawatch.com/products/main/800x800-r/201608/0003.jpg", int ratio = 2)
        {
            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(url);
                var eformat = SixLabors.ImageSharp.Image.DetectFormat(imageBytes);

                var image = SixLabors.ImageSharp.Image.Load(imageBytes);
                image.Mutate(ctx => ctx.Resize(image.Width / ratio, image.Height / ratio));
                var result = image.ToBase64String(eformat).Split(",")[1];
                return result;
            }
        }
    }
}
