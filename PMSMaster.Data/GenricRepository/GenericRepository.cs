using PMSMaster.Data.DataContext;
using log4net;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PMSMaster.Data.GenricRepository
{
    //public abstract class GenericAsyncRepository<T> : IGenericAsyncRepository<T> where T : class, IDisposable
    //{
    //    protected AblGlobalCoreContext _dbContext;

    //    public GenericAsyncRepository(AblGlobalCoreContext context)
    //    {
    //        _dbContext = context;
    //    }
    public abstract class GenericAsyncRepository<T> :
      IGenericAsyncRepository<T> where T : class
    {
        protected readonly ApplicationDBContext _dbContext;


        //   private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
        public GenericAsyncRepository(ApplicationDBContext context)
        {
            this._dbContext = context;
        }
        //public C Context
        //{
        //    get { return _dbContext; }
        //    set { _dbContext = value; }
        //}


        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }


        public virtual async Task<ICollection<T>> GetAllAsyn()
        {

            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual T Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual T Add(T t)
        {
            _dbContext.Set<T>().Add(t);
            _dbContext.SaveChanges();
            return t;
        }

        public virtual List<T> AddList(List<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }

        public virtual async Task<bool> AddAsyn(T t)
        {
            try
            {
                _dbContext.Set<T>().Add(t);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }



        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(match);
        }
        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().Where(match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual async Task<bool> DeleteAsyn(T entity)
        {
            try
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _dbContext.Set<T>().Find(key);
            if (exist != null)
            {
                _dbContext.Entry(exist).CurrentValues.SetValues(t);
                _dbContext.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsyn(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await _dbContext.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _dbContext.Entry(exist).CurrentValues.SetValues(t);
                await _dbContext.SaveChangesAsync();
            }
            return exist;
        }
        public virtual T Update(T t)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return t;
        }

        public virtual async Task<bool> UpdateAsyn(T t)
        {
            try
            {
                _dbContext.Entry(t).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public int Count()
        {
            return _dbContext.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public virtual void Save()
        {

            _dbContext.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<T>> GetAllAsyn(object p)
        {
            throw new NotImplementedException();
        }
    }
}
