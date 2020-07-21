using ImageManager.BLL.Interfaces.ImageStorageServices;
using ImageManager.DAL.Infrastructure;
using ImageManager.DTO.ImageStorageDTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ImageManager.Models.ImageStorageModels;
using ImageManager.API.Helpers;
using System.Drawing;
using ImageManager.Storage;
using System;
using ImageManager.Entities.ImageStore;
using System.Threading.Tasks;

namespace ImageManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {

        private readonly IImageStorageService imageStorageService;
        private readonly IMapper mapper;
        public StorageController(IUnitOfWork unitOfWork,
            IImageStorageService imageStorageService,
            IMapper mapper) : base(unitOfWork)
        {
            this.imageStorageService = imageStorageService;
            this.mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        [Route("GetImageById/{id}")]
        public JsonResult GetImageById(int id)
        {

            ImageStorageDTO imageStorageDTO = mapper.Map<ImageStorageDTO>(imageStorageService.GetImageById(id));
            ImageStorageViewModel imageStorageViewModel = mapper.Map<ImageStorageViewModel>(imageStorageDTO);
            return Json(imageStorageViewModel);
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<JsonResult> UploadImage(ImageStorageUploadViewModel model)
        {
            CloudinaryStorage cloudinaryStorage = new CloudinaryStorage();
            string url = cloudinaryStorage.Cloudinary(model.Image, model.Name);
            Guid guid = Guid.NewGuid();
            ImageStorageDTO imageStorageDTO = new ImageStorageDTO()
            {
                Name = model.Name,
                URL = url,
                ConcurrencyStamp = guid.ToString()
            };

            ImageStorage imageStorage = mapper.Map<ImageStorage>(imageStorageDTO);
            imageStorageService.Insert(imageStorage);

            await SaveAsync();

            return Json($"The Image Uploaded and is located at the following link \n {url}");
        }

    }
}
