using ImageManager.BLL.Interfaces;
using ImageManager.DAL.Infrastructure;
using ImageManager.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.BLL
{
    public abstract class Service<T> : IService<T>
      where T : class, IEntityBase
    {
        protected Service(IRepository<T> repository)
        {
            Repository = repository;
        }
        protected IRepository<T> Repository { get; private set; }
    }
}
