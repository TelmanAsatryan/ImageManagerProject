using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using ImageManager.Storage.Helpers;
using System.Linq;

namespace ImageManager.Storage
{
    public class CloudinaryStorage
    {
        public string Cloudinary(byte[] image, string name)
        {
            //IConfigurationBuilder builder = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("ClodyinaryConfig.json", optional: true, reloadOnChange: true);

            //IConfigurationRoot configuration = builder.Build();
            //configurationSection.Key => FilePath
            // configurationSection.Value => C:\\temp\\logs\\output.txt
            //IConfigurationSection configurationSection = configuration.GetSection("AppConfig");

            ImageConverter imageConverter = new ImageConverter();
            var config = new ConfigurationBuilder().AddJsonFile(@"C:\Users\TELMAN\source\repos\ImageManager\ImageManager.Storage\ClodyinaryConfig.json").Build().GetChildren();
            Account account = new CloudinaryDotNet.Account("damiiis6r", "591262355387469", "VBgPuS6TcTySIel8VWMBJazGNag");
            Cloudinary cloudinary = new Cloudinary(account);
            Image img = imageConverter.byteArrayToImage(image);


            MemoryStream ms = new MemoryStream(image);

            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, ms),
                UseFilename = true,
                UniqueFilename = false

            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

            return uploadResult.SecureUri.ToString();
        }


    }
}
