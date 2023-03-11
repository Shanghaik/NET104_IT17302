using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Project.Models;
namespace Shopping_Project.Configurations
{
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(p=>p.Product).WithMany(p=>p.CartDetail).
                HasForeignKey(p=>p.IdSP);
            builder.HasOne(p => p.Cart).WithMany(p => p.CartDetails).
                HasForeignKey(p => p.UserId);
        }
    }
}
