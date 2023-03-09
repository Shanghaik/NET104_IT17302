using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Project.Models;

namespace Shopping_Project.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<BIll>
    {
        public void Configure(EntityTypeBuilder<BIll> builder)
        {
            builder.ToTable("HoaDon"); // Đặt tên bảng
            builder.HasKey(p => p.Id); // Set khóa chính
            // Config cho thuộc tính
            builder.Property(p=>p.Status).HasColumnType("int").
                IsRequired(); // int not null
            builder.Property(p => p.CreateDate).HasColumnType("Datetime");
        }
    }
}
