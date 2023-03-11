using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Shopping_Project.Models
{
    public class ShopDbContext: DbContext
    {
        public ShopDbContext(){}
        public ShopDbContext(DbContextOptions options):base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<BIll> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=SHANGHAIK;Initial Catalog=IT17302_ShopDuyBug;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.
                GetExecutingAssembly());
        }
    }
}
