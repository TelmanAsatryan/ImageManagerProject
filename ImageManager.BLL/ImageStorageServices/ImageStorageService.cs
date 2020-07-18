using ImageManager.BLL.Interfaces.ImageStorageServices;
using ImageManager.DAL.Infrastructure;
using ImageManager.Entities.ImageStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageManager.BLL.ImageStorageServices
{
    public class ImageStorageService : Service<ImageStorage>, IImageStorageService
    {
        public ImageStorageService(IRepository<ImageStorage> repository) : base(repository)
        {
        }

        public ImageStorage GetImageById(int id)
        {
            return Repository.Query()
                .Where(w => !w.IsDeleted && w.Id == id)
                .Get()
                .FirstOrDefault();
        }

        public void Insert(ImageStorage entity)
        {
            Repository.InsertAsync(entity);
        }

        public void Update(ImageStorage entity)
        {
            Repository.Update(entity);
        }
    }
}
