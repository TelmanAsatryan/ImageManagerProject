using ImageManager.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ImageManager.DAL.Infrastructure
{
    interface RepositoryQuery
    {
    }
    public sealed class RepositoryQuery<TEntity>
       where TEntity : class, IEntityBase
    {
        private readonly List<Expression<Func<TEntity, object>>>
            includeProperties;

        private readonly Repository<TEntity> repository;
        private Expression<Func<TEntity, bool>> filter;
        private Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderByQuerable;
        private int? page;
        private int? pageSize;

        public RepositoryQuery(Repository<TEntity> repository)
        {

            this.repository = repository;
            includeProperties =
                new List<Expression<Func<TEntity, object>>>();
        }

        public RepositoryQuery<TEntity> Where(
            Expression<Func<TEntity, bool>> filter, CombineOption option = CombineOption.And)
        {
            if (this.filter == null)
            {
                this.filter = filter;
            }
            else
            {
                switch (option)
                  {
                    //case CombineOption.And: { this.filter = this.filter.And(filter); break; }
                    //case CombineOption.Or: { this.filter = this.filter.Or(filter); break; }
                }
            }
            return this;
        }

        public RepositoryQuery<TEntity> OrderBy(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            orderByQuerable = orderBy;
            return this;
        }


        public RepositoryQuery<TEntity> Include(
            Expression<Func<TEntity, object>> expression)
        {
            includeProperties.Add(expression);
            return this;
        }

        public IEnumerable<TEntity> GetAsNoTracking()
        {
            return repository
                .Get(filter, orderByQuerable, includeProperties, page, pageSize).AsNoTracking();
        }
        public IEnumerable<TEntity> GetPage(
            int page, int pageSize, out int totalCount)
        {
            this.page = page;
            this.pageSize = pageSize;
            totalCount = repository.Get(filter: filter, includeProperties: includeProperties)
                .Count();

            return repository.Get(
                filter,
                orderByQuerable, includeProperties, page, pageSize)
                .ToList();
        }

        public IEnumerable<TResult> GetAs<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return repository
                .Get(filter, orderByQuerable, includeProperties, page, pageSize)
                .Select(selector)
                .ToList();
        }

        public int Count()
        {
            return repository
                .Get(filter: filter, includeProperties: includeProperties)
                .Count();
        }

        public IEnumerable<TEntity> Get()
        {
            return repository
                .Get(filter, orderByQuerable, includeProperties, page, pageSize)
                .ToList();
        }
    }
}
