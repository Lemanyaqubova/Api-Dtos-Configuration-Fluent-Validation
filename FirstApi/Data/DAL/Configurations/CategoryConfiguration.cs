using FirstApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstApi.Data.DAL.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property( c => c.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(c => c.Description).HasMaxLength(50).IsRequired(true);
        }
    }
}
