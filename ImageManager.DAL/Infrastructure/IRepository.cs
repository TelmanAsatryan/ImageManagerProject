using ImageManager.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.DAL.Infrastructure
{
    public interface IRepository<T> where T : class, IEntityBase
    {
        T FindById(object id);
        void InsertGraph(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateSingleProp(T entity, string propertyName);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(object id);
        void DeleteRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteForever(IEnumerable<T> entities);
        void Insert(T entity);
        Task InsertAsync(T entity);
        RepositoryQuery<T> Query();
        void Destroy(T entity);

    }
}
