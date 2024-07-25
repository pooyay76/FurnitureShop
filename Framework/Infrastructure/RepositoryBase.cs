using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Infrastructure
{
    public class RepositoryBase<Tkey, T> : IRepository<Tkey, T> where T : class

    {
        private readonly DbContext context;

        public RepositoryBase(DbContext context)
        {
            this.context = context;
        }

        public void Create(T entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            context.Remove<T>(entity);
            context.SaveChanges();
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Any(expression);
        }

        public T Find(params object[] primaryKeys)
        {
            return context.Find<T>(primaryKeys);
        }
        public void Update(T entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
        public T Get(Tkey id)
        {
            return context.Find<T>(id);
        }
    }
}
