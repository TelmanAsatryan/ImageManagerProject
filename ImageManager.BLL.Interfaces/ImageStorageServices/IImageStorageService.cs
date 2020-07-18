using ImageManager.Entities.ImageStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.BLL.Interfaces.ImageStorageServices
{
    public interface IImageStorageService : IService<ImageStorage>
    {
        void Insert(ImageStorage entity);
        void Update(ImageStorage entity);
        ImageStorage GetImageById(int id);
    }
}
