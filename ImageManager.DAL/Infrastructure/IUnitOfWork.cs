using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.DAL.Infrastructure
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
    }
}
