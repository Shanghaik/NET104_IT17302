﻿using Microsoft.AspNetCore.Mvc;
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
            var data = HttpContext.Session.GetString("thongbao");
            if(data == null)
            {
                ViewData["message"] = "Session không tồn tại hoặc đã timeout";
            }else 
            ViewData["message"] = data;
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
        // Các ảnh không nằm trong thư mục root khi chạy sẽ không hiển thị
        // ra với các phương thức cơ bản => để hienr thị được ta cần phải
        // thực hiện cách sau: Lấy đường dẫn ảnh => copy ảnh đó vào wwwroot
        // sau đó thực hiện hiển thị như bình thường
        public IActionResult Create(Product product, IFormFile imageFile) // Thực hiện thêm
        {
            // Trong trường hợp chúng ta thực hiện với thuộc tính Description
            // Thuộc tính này đang là string => Không thể thao tác trực tiếp
            // với các file => Truyền thêm 1 tham số vào Action này
            // Truyền thêm 1 tham số imageFile kiểu IFormFile
            // Bước 1: Kiểm tra đường dãn tới ảnh được lấy từ form
            if(imageFile != null && imageFile.Length > 0) // Không null không rỗng
            {
                // Thực hiện trỏ tới thư mục root để lát thực hiện việc copy
                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", imageFile.FileName); // Bước 2
                // Kết quả: aaa/wwwroot/images/xxx.jpg
                var stream = new FileStream(path, FileMode.Create);
                // Vì chúng ta thực hiện việc copy => Tạo mới => Create
                imageFile.CopyTo(stream); // Copy ảnh chọn ở form vào wwwroot/images
                // Gán lại giá trị cho thuộc tính Description => Bước 3
                product.Description = imageFile.FileName; // Bước 4
            }
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
        public IActionResult SessionTest()
        {
            // Session - phiên làm việc, khi dữ liệu được đẩy vào Session
            // thì dữ liệu này sẽ tồn tại cho đến khi phiên làm việc kết thúc
            string message = "Chúng ta đang có kèo import được cookies 1 triệu";
            // Truyền một string data vào session
            HttpContext.Session.SetString("thongbao", message);
            // Lấy data từ Session để sử dụng
            var data = HttpContext.Session.GetString("thongbao");
            ViewData["message"] = data;
            // Nguyên tắc tính thời gian timeout của Session
            // Khi chúng ta đã có dữ liệu trong Session thì bộ đếm thời gian 
            // sẽ được kích hoạt ngay khi request cuối cùng được thực thi
            // Nếu sau khoảng thời gian giới hạn timeout mà không có thêm
            // requets nào được thực hiện thì session sẽ bị xóa
            // Nếu trước thời điểm timeout, có 1 requets nào đó được thực thi
            // thì bộ đếm sẽ được reset
            // Khi muốn chủ động xóa Session ta có 2 cách
            // 1. Xóa tất
            // HttpContext.Session.Clear();
            // 2. Xóa theo key cụ thể
            // HttpContext.Session.Remove("thongbao");
            return View();
            // Thử truyền 1 list đối tượng vào Session sau đó đọc lại
        }

        public IActionResult AddToCart(Guid id)
        {
            // Các bước thực hiện thêm giỏ hàng với session
            // Bước 1: Xác nhận sản phẩm theo ID
            var product = productServices.GetProductById(id);
            // Bước 2: Lấy ra danh sách các SP trong giỏ hàng
            var products = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
            // Bước 3: Sau khi xác nhận thêm / Ghi đè vào session
            if(products.Count == 0)
            {
                products.Add(product); // thêm sp vào list để ghi trực tiếp vào session
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            }
            else
            {
                if(SessionServices.CheckObjInList(id, products))
                { // Kiểm tra xem sản phẩm có Id truyền vào đã nằm trong list ở Session chưa?
                    return Content("Bình thường thì sẽ update số lượng nhưng ở đây chỉ thông báo thôi");
                } else // trong trường hợp sản phẩm chưa nằm trong giỏ hàng thì thêm vào
                {
                    products.Add(product); // thêm sp vào list để ghi đè vào session
                    SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult ShowCart()
        {
            var products = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
            return View(products);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}