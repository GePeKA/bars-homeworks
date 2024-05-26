using Microsoft.AspNetCore.Identity;

namespace TaskManager.Identity
{
    public sealed class AppRole : IdentityRole<long>
    {
        public AppRole(string roleName)
        {
            Name = roleName;
            NormalizedName = roleName.ToUpperInvariant();
        }

        private AppRole() { }
    }
}
