using Microsoft.EntityFrameworkCore;
using TaskManager.DataAccess.Configurations;
using Task = TaskManager.Entities.Task;

namespace TaskManager.DataAccess
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Task> Tasks => Set<Task>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
