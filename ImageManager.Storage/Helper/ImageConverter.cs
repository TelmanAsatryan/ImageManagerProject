using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageManager.Storage.Helpers
{
    public class ImageConverter
    {
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream mStream = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(mStream);
            }
        }
    }
}
