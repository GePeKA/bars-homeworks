namespace TaskManager.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync();
    }
}
