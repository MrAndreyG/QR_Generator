﻿namespace QR_Generator.Models
{
    public class ImageData
    {
        public byte[]? data { get; set; }
        public string ImageSRC_Data { get; set; }
        ~ImageData()
        {
            data = null;
            Console.WriteLine("Finalizing object");
        }
        

    }

}
