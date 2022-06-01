using Microsoft.AspNetCore.Mvc;
using QR_Generator.Models;
using System.Diagnostics;
using QRCoder;
using QRCoder.Extensions;
using QRCoder.Exceptions;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;





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
            vm.data = new byte[1024];

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
        public IActionResult Create(ImageData model)
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



        [HttpPost]
        public JsonResult CopyDataFromMStoPQ()
        {
            string Name = "Start of copying";
            Name = Name + "!";

            Uri generator = new Uri("http://nord-tranzit.ru");
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("http://nord-tranzit.ru", QRCodeGenerator.ECCLevel.Q);


            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            //byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);

            string ReturnSRC = $"data:image/jpeg;base64,{Convert.ToBase64String(qrCodeAsBitmapByteArr)}";

            return Json(ReturnSRC);
        }



    }
}