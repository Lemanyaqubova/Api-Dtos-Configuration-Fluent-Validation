using FirstApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstApi.Data.DAL.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(p=>p.SalePrice).IsRequired(true);
            builder.Property(p => p.CreateDate).HasDefaultValueSql("GetUtcDate()");
            builder.Property(p => p.UpdateDate).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
