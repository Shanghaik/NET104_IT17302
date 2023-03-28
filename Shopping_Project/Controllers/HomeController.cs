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

        public IActionResult Details(Guid id)
        {
            var products = productServices.GetProductById(id);
            return View(products);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Product p = productServices.GetProductById(id);
            return View(p);
        }

        public IActionResult Edit(Product p)
        {
            if (productServices.UpdateProduct(p))
                return RedirectToAction("ShowAllProducts");
            return BadRequest();
        }

        public IActionResult Test_ViewBag_ViewData()
        {
            var listBag = productServices.GetAllProducts();
            ViewBag.Products = listBag;
            // Với viewbag, ta không cần khởi tạo mà chỉ cần sử dụng
            // trực tiếp như 1 thuộc tính static, trong trường hợp
            // này, ViewBag.Products được sử dụng trực tiếp mà không
            // cần quan tâm xem nó được tạo ra như thế nào.
            // Dữ liệu ở viewbag là dữ liệu dynamic
            var listData = productServices.GetAllProducts();
            // ViewData dữ liệu sẽ ở dạng Generic, hoạt động theo cơ
            // chế key-value
            ViewData["Products"] = listData; // "Product" là key, listData là value
            // ViewBag - ViewData chỉ dùng được trong phạm vi của View đó
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}