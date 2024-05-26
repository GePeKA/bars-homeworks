using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Identity
{
    public class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : IdentityDbContext<AppUser, AppRole, long>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedRoles(builder);

            base.OnModelCreating(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<AppRole>().HasData(
                new AppRole("user") { Id = 1 },
                new AppRole("admin") { Id = 2 }
            );
        }
    }
}
