using Shopping_Project.Models;

namespace Shopping_Project.IServices
{
    public interface IProductServices
    {
        public bool CreateProduct(Product p);
        public bool UpdateProduct(Product p);
        public bool DeleteProduct(Guid id);
        public List<Product> GetAllProducts();  
        public Product GetProductById(Guid id);
        public List<Product> GetProductByName(string name);


    }
}
