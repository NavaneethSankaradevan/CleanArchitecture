using MSF.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MSF.Application
{
    internal class UserContext : IdentityDbContext<AppUser>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);

            // Seed Roles.
            foreach (string r in Global.RoleList)
            {
                builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = r, NormalizedName = r.ToUpper() });
            }
        }
    }
}
