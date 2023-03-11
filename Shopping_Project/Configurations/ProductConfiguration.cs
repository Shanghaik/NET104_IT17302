using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Project.Models;
namespace Shopping_Project.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p=>p.Id);
            builder.Property(p => p.Name).
                HasColumnType("nvarchar(100)");
            builder.Property(p => p.BillDetail).IsUnicode(true).
                IsFixedLength().HasMaxLength(100); // nvarchar(100)
            // nchar(100) vs nvarchar(100)
            builder.Property(p => p.Description).
                HasColumnType("nvarchar(100)");
        }
    }
}
