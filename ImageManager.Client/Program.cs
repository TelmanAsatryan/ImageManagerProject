using ImageManager.Models.ImageStorageModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Drawing;
using System.IO;

namespace ImageManager.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please Write What You Want To Do (DOWNLOAD,UPLOAD,EXIT)");
            string process = Console.ReadLine();
            while (process != "EXIT")
            {
                switch (process)
                {

                    case "DOWNLOAD":

                        Console.WriteLine("Please Enter Image Name");
                        string name = Console.ReadLine();
                        Console.WriteLine("Please Enter Image Local Path");
                        string ImagePath = Console.ReadLine();
                        Image img = Image.FromFile(ImagePath);
                        byte[] bArr = imgToByteArray(img);
                        ImageStorageUploadViewModel imageStorageUploadViewModel = new ImageStorageUploadViewModel();
                        imageStorageUploadViewModel.Name = name;
                        imageStorageUploadViewModel.Image = bArr;
                        Upload(imageStorageUploadViewModel);
                        break;
                    case "UPLOAD":

                        break;
                }
            }
            Console.WriteLine("Thanks For Using Our Program (click ENTER to exit)");
            Console.ReadKey();
        }


        public static void Upload(ImageStorageUploadViewModel model)
        {
            var client = new RestClient("https://localhost:44363/api/Storage/UploadImage");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }


        public static byte[] imgToByteArray(Image img)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, img.RawFormat);
                return mStream.ToArray();
            }
        }
    }
}
