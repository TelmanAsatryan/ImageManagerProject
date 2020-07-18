using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.Models.ImageStorageModels
{
    public class ImageStorageUploadViewModel : IModelBase
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}
