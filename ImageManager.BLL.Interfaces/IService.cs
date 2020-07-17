using ImageManager.Entities;
using System;

namespace ImageManager.BLL.Interfaces
{
    public interface IService
    {
    }

    public interface IService<T> : IService
        where T : IEntityBase
    {

    }
}
