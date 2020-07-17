using ImageManager.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageManager.DAL.Infrastructure
{
    public class Repository<T> : IRepository<T>
         where T : class, IEntityBase
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public virtual T FindById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void InsertGraph(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            context.Entry(entity).State = EntityState.Added;
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            context.Entry(entity).State = EntityState.Modified;
            dbSet.Update(entity);
        }

        public void UpdateSingleProp(T entity, string propertyName)
        {
            context.Entry(entity).Property(propertyName).IsModified = true;
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.UpdatedDate = DateTime.Now;
                context.Entry(entity).State = EntityState.Modified;
            }
            dbSet.UpdateRange(entities);
        }

        public virtual void Delete(object id)
        {
            var entity = dbSet.Find(id);
            Delete(entity);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.UpdatedDate = DateTime.Now;
                entity.IsDeleted = true;
                context.Entry(entity).State = EntityState.Modified;
                dbSet.Update(entity);
            }
        }
        public virtual void DeleteForever(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {

                context.Entry(entity).State = EntityState.Deleted;
                dbSet.RemoveRange(entity);
            }
        }
        public virtual void Delete(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.IsDeleted = true;
            context.Entry(entity).State = EntityState.Modified;

            dbSet.Update(entity);
        }

        public virtual void Destroy(T entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
            dbSet.Remove(entity);
        }

        public virtual void Insert(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            entity.IsDeleted = false;
            context.Entry(entity).State = EntityState.Added;
            dbSet.Add(entity);
        }

        public virtual async Task InsertAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            entity.IsDeleted = false;
            context.Entry(entity).State = EntityState.Added;
            await dbSet.AddAsync(entity);
        }

        public virtual RepositoryQuery<T> Query()
        {
            var repositoryGetFluentHelper =
                new RepositoryQuery<T>(this);

            return repositoryGetFluentHelper;
        }

        internal IQueryable<T> Get1(
             Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>,
                 IOrderedQueryable<T>> orderBy = null,
             Dictionary<Expression<Func<T, object>>, List<Expression<Func<object, object>>>>
                 includes = null,
             int? page = null,
             int? pageSize = null,
             bool asNoTracking = false,
             bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = dbSet;

            //	if (ignoreQueryFilters) dbSet.IgnoreQueryFilters();
            foreach (var inc in includes)
            {
                var inner = query.Include(inc.Key);
                if (inc.Value.Any())
                {
                    foreach (var tInc in inc.Value)
                    {
                        inner = inner.ThenInclude(tInc);
                    }
                }
                query = inner;
            }

            //query.ThenInclude();
            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return asNoTracking ? query.AsNoTracking() : query;
        }


        internal IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,
                IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>>
                includeProperties = null,
            int? page = null,
            int? pageSize = null,
            bool asNoTracking = false,
            bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = dbSet;


            includeProperties?.ForEach(i =>
            {
                string path = new PropertyPathVisitor().GetPropertyPath(i);
                query = query.Include(path);
            });
            //query.ThenInclude();
            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return asNoTracking ? query.AsNoTracking() : query;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            //context.Set<T>().AddRange(entities); // TODO
            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = DateTime.Now;
                entity.IsDeleted = false;
            }
            await dbSet.AddRangeAsync(entities);
        }

    }
    class PropertyPathVisitor : ExpressionVisitor
    {
        private Stack<string> _stack;

        public string GetPropertyPath(Expression expression)
        {
            _stack = new Stack<string>();
            Visit(expression);
            return _stack
                .Aggregate(
                    new StringBuilder(),
                    (sb, name) =>
                        (sb.Length > 0 ? sb.Append(".") : sb).Append(name))
                .ToString();
        }

        protected override Expression VisitMember(MemberExpression expression)
        {
            if (_stack != null)
                _stack.Push(expression.Member.Name);
            return base.VisitMember(expression);
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (IsLinqOperator(expression.Method))
            {
                for (int i = 1; i < expression.Arguments.Count; i++)
                {
                    Visit(expression.Arguments[i]);
                }
                Visit(expression.Arguments[0]);
                return expression;
            }
            return base.VisitMethodCall(expression);
        }

        private static bool IsLinqOperator(MethodInfo method)
        {
            if (method.DeclaringType != typeof(Queryable) && method.DeclaringType != typeof(Enumerable))
                return false;
            return Attribute.GetCustomAttribute(method, typeof(ExtensionAttribute)) != null;
        }

    }
}
