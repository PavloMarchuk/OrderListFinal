using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreGenericRepository.Common
{
    public interface IGenericRepository<T> where T : class
    {
        T Add(T t);
        Task<T> AddAsyn(T t);

        T Update(T t, object key);
        Task<T> UpdateAsyn(T t, object key);

        void Delete(T entity);
        Task<int> DeleteAsyn(T entity);


        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsyn();

        T Get(object id);
        Task<T> GetAsync(object id);

        void Save();
        Task<int> SaveAsync();


    }
}
