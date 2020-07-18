using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageManager.BLL.Interfaces.ImageStorageServices;
using ImageManager.DAL.Infrastructure;
using ImageManager.DTO.ImageStorageDTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ImageManager.Models.ImageStorageModels;
using Microsoft.AspNetCore.Http;
using ImageManager.API.Helpers;
using System.Drawing;

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
        public JsonResult UploadImage(ImageStorageUploadViewModel model)
        {
            ImageConverter imageConverter = new ImageConverter();
            Image img = imageConverter.byteArrayToImage(model.Image);
            return Json("The Image Uploaded");
        }

    }
}
