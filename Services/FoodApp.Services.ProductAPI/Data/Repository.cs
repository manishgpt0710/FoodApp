using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FoodApp.Services.ProductAPI.Data
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetByIdAsync(long id, bool includeChildren = false);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
        void DeleteRangeAsync(IEnumerable<T> entities);
    }
    public partial class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> entities;

        public Repository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await entities.AsNoTracking().Where(expression).ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(long id, bool includeChildren = false)
        {
            return await entities.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            await entities.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entityList)
        {
            if (entityList == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            await entities.AddRangeAsync(entityList);
        }

        public void UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            context.Entry(entity).State = EntityState.Modified;
            // await context.SaveChangesAsync();
        }

        public void DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            entities.Remove(entity);
            // await context.SaveChangesAsync();
        }

        public void DeleteRangeAsync(IEnumerable<T> entityList)
        {
            if (entityList == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            entities.RemoveRange(entityList);
            // await context.SaveChangesAsync();
        }
    }
}
