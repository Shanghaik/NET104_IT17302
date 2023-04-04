using Shopping_Project.IServices;
using Shopping_Project.Models;

namespace Shopping_Project.Services
{
    public class ProductServices : IProductServices
    {
        ShopDbContext context;// = new ShopDbContext();
        public ProductServices()
        {
            context = new ShopDbContext();
        }
        public bool CreateProduct(Product p)
        {
            try
            {
                context.Products.Add(p);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProduct(Guid id)
        {
            try
            {// Find(id) chỉ dùng cho thuộc tính khóa chính là id
                var product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetAllProducts()
        {
            return context.Products.ToList();
        }

        public Product GetProductById(Guid id)
        {
            return context.Products.FirstOrDefault(p => p.Id == id);
            // return context.Products.SingleOrDefault(p => p.Id == id);
        }

        public List<Product> GetProductByName(string name)
        {
            return context.Products.Where(p => p.Name.Contains(name)).ToList();
        }

        public bool UpdateProduct(Product p)
        {
            try
            {
                var product = context.Products.Find(p.Id);
                product.Name = p.Name;
                product.Supplier = p.Supplier;
                product.Price = p.Price;
                product.Description = p.Description;
                context.Update(product);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
