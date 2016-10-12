using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebFor.Core.Repository
{
    public interface IRepository<T, in TKey> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        Task<T> FindByIdAsync(TKey id);
        Task <List<T>> GetAllAsync();

        // redundant, redundant, Unit Of Work will handle it
        //void Save();

        // too generic, hard to implement for other providers
        //IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
