using Microsoft.AspNetCore.Mvc;
using Shopping_Project.IServices;
using Shopping_Project.Models;
using Shopping_Project.Services;
using System.Diagnostics;

namespace Shopping_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServices productServices; // interface
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            productServices = new ProductServices(); // Class
        }

        public IActionResult Index()
        {
            return View(); // Hiển thị View khi Action được gọi đến
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View(); // Gọi đến đúng View có tên như Action
        }

        public IActionResult Redirect()
        {
            // Một các câu lệnh xử lý nào đó mà không liên quan tới view
            return RedirectToAction("Test"); // Chuyển hướng tói 1 Action khác
        }

        public IActionResult Show()
        {
            // Đẩy dữ liệu từ action qua view thông qua model object
            // Có obj
            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Học lại", 
                AvailableQuantity = 1,
                Supplier = "MamaBank",
                Price = 672000, 
                Description = "Trượt điểm danh",
                Status = new Random().Next(0, 100)
            };
            return View(product);  // Truyền thẳng vào view luôn
            // 1 View truyền dc trực tiếp 1 Object model
        }
        public IActionResult ShowAllProducts()
        {
            // Đẩy dữ liệu từ action qua view thông qua model object
            // Có obj
            List<Product> products = productServices.GetAllProducts();
            return View(products);  // Truyền thẳng vào view luôn
            // 1 View truyền dc trực tiếp 1 Object model
        }

        public IActionResult Create() // Hiển thị form ra cho người dùng thôi
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product) // Thực hiện thêm
        {
            if (productServices.CreateProduct(product))
            {
                return RedirectToAction("ShowAllProducts");
            }
            else return BadRequest();
        }

        public IActionResult Delete(Guid id)
        {
            if (productServices.DeleteProduct(id))
            {
                return RedirectToAction("ShowAllProducts");
            }
            else return BadRequest();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}