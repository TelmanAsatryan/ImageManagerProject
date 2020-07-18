using AutoMapper;
using ImageManager.DTO.ImageStorageDTOs;
using ImageManager.Models.ImageStorageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.API.Mapper.ImageStorageProfiles
{
    public class ImageStorageProfile : Profile
    {
        public ImageStorageProfile()
        {
            CreateMap<ImageStorageDTO, ImageStorageViewModel>().ReverseMap();
        }
    }
}
