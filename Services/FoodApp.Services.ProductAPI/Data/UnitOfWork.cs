namespace FoodApp.Services.ProductAPI.Data
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        IRepository<T> Repository<T>() where T : class;
    }
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private readonly AppDbContext _dbContext = dbContext;

        public IRepository<T> Repository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
