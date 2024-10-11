using System.Linq.Expressions;

namespace PMSMaster.Data.GenricRepository
{
    public interface IGenericAsyncRepository<T> where T : class
    {
        T Add(T t);
        List<T> AddList(List<T> entities);
        Task<bool> AddAsyn(T t);
        int Count();
        Task<int> CountAsync();
        void Delete(T entity);
        Task<bool> DeleteAsyn(T entity);
        void Dispose();
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);
        T Get(int id);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsyn(object p);
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(int id);
        void Save();
        Task<int> SaveAsync();
        T Update(T t, object key);

        Task<T> UpdateAsyn(T t, object key);
        T Update(T t);
        Task<bool> UpdateAsyn(T t);

    }
}
