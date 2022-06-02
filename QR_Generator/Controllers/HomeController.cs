using Microsoft.AspNetCore.Mvc;
using QR_Generator.Models;
using System.Diagnostics;
using QRCoder;


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;




namespace QR_Generator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var vm = new ImageData();
            vm.data = new byte[10];

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpPost]
        public IActionResult Create1(ImageData model)
        {
            //  check model.EmployeeId 
            //  to do : Save and redirect
            model = model;

            var vm = new ImageData();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);
            vm.data = qrCodeAsBitmapByteArr;

            return View("Index", vm);
        }

        public IActionResult Create(ImageData model)
        {
            string Name = "Start of copying";
            Name = Name + "!";
            string ReturnSRC = String.Empty;
            ImageData ImgData = new ImageData();



            Uri generator = new Uri("http://nord-tranzit.ru");
            string payload = generator.ToString();

            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                //QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
                //QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
                QRCodeData qrCodeData = qrGenerator.CreateQrCode("http://nord-tranzit.ru", QRCodeGenerator.ECCLevel.Q);


                BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
                byte[] qrCodeAsBitmapByteArr = null;
                //byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);
                ImgData.data = qrCode.GetGraphic(20);

                //ReturnSRC = $"data:image/jpeg;base64,{Convert.ToBase64String(qrCodeAsBitmapByteArr)}";
                ReturnSRC = $"data:image/jpeg;base64,{Convert.ToBase64String(ImgData.data)}";
                //ReturnSRC = $"--";

                qrGenerator.Dispose();
                qrCodeData.Dispose();
                qrCode.Dispose();
            }
            finally
            {
                //ImgData
            }

            return View("Index", ImgData);
        }



        [HttpPost]
        public JsonResult CopyDataFromMStoPQ()
        {
            string Name = "Start of copying";
            Name = Name + "!";
            string ReturnSRC = String.Empty;
            ImageData ImgData = new ImageData();

            try
            {
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode("http://www.nord-build.ru", QRCodeGenerator.ECCLevel.Q))
                    {
                        using (BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData))
                        {
                            byte[] qrCodeAsBitmapByteArr = null;
                            //byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);
                            ImgData.data = qrCode.GetGraphic(20);
                            //var qrCodeAsBitmap = qrCode.GetGraphic(20);


                            //ReturnSRC = $"data:image/jpeg;base64,{Convert.ToBase64String(qrCodeAsBitmapByteArr)}";
                            ReturnSRC = $"data:image/jpeg;base64,{Convert.ToBase64String(ImgData.data)}";
                            //ReturnSRC = $"--";
                        }
                    }
                }
            }
            finally {
                //ImgData
                
            }
           

            return Json(ReturnSRC);
        }


    }
}