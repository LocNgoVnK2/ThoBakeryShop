using System.Linq;

namespace Infrastructure.Generic
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetAll();
    }


}
