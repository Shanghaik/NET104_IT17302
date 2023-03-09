using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Project.Models;
namespace Shopping_Project.Configurations
{
    public class BillDetailsConfiguration : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).HasColumnType("int");
            // Set khóa ngoại
            builder.HasOne(p => p.BiLL).WithMany(x => x.BillDetail).
                HasForeignKey(k => k.IdHD).HasConstraintName("FK_HD");
            //builder.HasOne<Product>().WithMany()
            //    .HasForeignKey(p => p.IdSP);
            builder.HasOne(p => p.Product).WithMany(x => x.BillDetail).
                HasForeignKey(k => k.IdSP).HasConstraintName("FK_SP");
        }
    }
}
