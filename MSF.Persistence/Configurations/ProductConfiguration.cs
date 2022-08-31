using MSF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MSF.Persistence
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(t => t.ProductName).IsUnique();

            builder.Property(t => t.ProductName).IsRequired().HasMaxLength(80);
            builder.Property(t => t.CategoryId).IsRequired();
            builder.Property(t => t.MRPPrice).HasDefaultValue(0);
            builder.Property(t => t.MinimumQuantity).HasDefaultValue(0);

        }
    }
}
