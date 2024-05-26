using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Identity.Extensions
{
    public static class IdentityRegisterExtension
    {
        public static IServiceCollection AddIdentity(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddDbContext<AppIdentityDbContext>(builder =>
            {
                builder.UseNpgsql(configuration["Database:Identity"]);
            });

            serviceCollection.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequiredLength = 8,
                    RequireNonAlphanumeric = true,
                    RequireLowercase = true,
                    RequireUppercase = false,
                    RequireDigit = true
                };
                options.User.RequireUniqueEmail = true;
                //options.SignIn.RequireConfirmedEmail = true;
            })
                .AddRoles<AppRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            return serviceCollection;
        }
    }
}
