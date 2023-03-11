using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping_Project.Models;
namespace Shopping_Project.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Username).HasColumnType("varchar(256)");
            builder.Property(p => p.Password).HasColumnType("varchar(256)");
            builder.HasOne(p=>p.Role).WithMany(p=>p.Users).
                HasForeignKey(p=>p.RoleId);


        }
    }
}
