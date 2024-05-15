using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Repositories;
using TaskManager.DataAccess.Repositories;

namespace TaskManager.DataAccess.Extensions
{
    public static class DbRegisterExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection,
        IConfiguration configuration)
        {
            serviceCollection.AddScoped<ITaskRepository, TaskRepository>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            return serviceCollection.AddDbContext<AppDbContext>(builder =>
            {
                builder.UseNpgsql(configuration["Database:ConnectionString"]);
                builder.UseSnakeCaseNamingConvention();
            });
        }
    }
}
