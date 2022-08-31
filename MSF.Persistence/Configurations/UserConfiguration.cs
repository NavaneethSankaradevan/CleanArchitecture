using MSF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSF.Persistence
{
    internal class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUser");
            
            builder.HasIndex(t => t.Email).IsUnique();
            builder.HasIndex(t => t.UserName).IsUnique();
            builder.Property(t => t.Id).HasColumnName("UserId");
            builder.Property(t => t.Email).IsRequired().HasMaxLength(60);
            builder.Property(t => t.FirstName).IsRequired().HasMaxLength(35);
            builder.Property(t => t.LastName).IsRequired().HasMaxLength(35);
            builder.Property(t => t.UserName).IsRequired().HasMaxLength(35);
        }
    }
}
