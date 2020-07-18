using AutoMapper;
using ImageManager.DTO.ImageStorageDTOs;
using ImageManager.Entities.ImageStore;


namespace ImageManager.BLL.Mapper.ImageStorageProfiles
{
    public class ImageStorageProfile : Profile
    {
        public ImageStorageProfile()
        {
            CreateMap<ImageStorage, ImageStorageDTO>().ReverseMap();
        }
    }
}
