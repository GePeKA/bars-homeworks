using TaskManager.Abstractions.Repositories;

namespace TaskManager.DataAccess.Repositories
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
