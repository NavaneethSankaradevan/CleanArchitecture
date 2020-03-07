using MSF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSF.Application
{
    internal class PersonConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Person
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasIndex(c => c.ContactNumber).IsUnique();
            builder.HasIndex(c => c.EMailAddress).IsUnique();
            builder.Property(c => c.EMailAddress).IsRequired().HasMaxLength(100);
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(25);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(25);
            builder.Property(c => c.ContactNumber).HasMaxLength(20);
        }
    }
}
