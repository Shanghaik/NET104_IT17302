using Shopping_Project.IServices;
using Shopping_Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductServices, ProductServices>();
// Có 3 hình thức áp dụng cho Dependency Injection
/* 
 * Add Transient: Khi có 1 request (yêu cầu mới). Mỗi yêu cầu HTTP request
 * sẽ nhận một đối tượng services khác nhau, loại này phù hợp cho các services 
 * mà không giữ trạng thái cố định và có tính chất phục vụ được cho nhiều loại
 * reuqest.
 * Add Scoped: Services tạo mới cho mỗi yêu cầu http request và sẽ giữ nguyên
 * trong suốt quá trình xử lý yêu cầu đó. Phù hợp cho các services mà có thể
 * giải quyết các yêu cầu http cụ thể nào đó.
 * Add Singleton: Services chỉ được tạo 1 lần trong suốt vòng đời của ứng dụng
 * các yêu cầu phía sau sẽ được nhận cùng 1 đối tượng services đó. Phù hợp cho 
 * các services có tính toàn cục và không thay đổi thường xuyên.
 */
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(20);
});
// Sử dụng Session với thời gian timeout là 20 giây
var app = builder.Build(); //Build cuối cùng

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession(); // thêm
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
