using System;
using System.Linq.Expressions;

namespace Framework.Domain
{
    public interface IRepository<Tkey, T> where T : class
    {
        public void Delete(T entity);
        public void Create(T entity);
        public bool Exists(Expression<Func<T, bool>> expression);
        public void Update(T entity);
        public T Find(params object[] keys);
        public T Get(Tkey key);
    }
}
