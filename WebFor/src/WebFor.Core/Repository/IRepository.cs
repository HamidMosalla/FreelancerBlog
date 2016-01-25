using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebFor.Core.Repository
{
    public interface IRepository<T, in TKey> where T : class
    {
        void Add(T entity);

        void Remove(T entity);

        T FindById(TKey id);
        Task<T> FindByIdAsync(TKey id);

        IEnumerable<T> GetAll();
        Task <List<T>> GetAllAsync();

        //void Update(T entity); redundant, Unit Of Work will handle it
        //void Save(); redundant, redundant, Unit Of Work will handle it
        //IQueryable<T> Find(Expression<Func<T, bool>> predicate); too generic, hard to implement for other providers
    }
}
