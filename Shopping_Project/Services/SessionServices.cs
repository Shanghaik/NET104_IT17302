using Newtonsoft.Json;
using Shopping_Project.Models;

namespace Shopping_Project.Services
{
    public static class SessionServices
    {
        // Lấy ra danh sách các SP từ session
        public static List<Product> GetObjFromSession(ISession session, string key)
        {
            string jsonData = session.GetString(key); // Lấy data dạng string từ Session
            if(jsonData == null ) // Chưa có gì trong Session
            {
                return new List<Product>(); // Trả về 1 danh sách sản phẩm rỗng
            }
            else{
                // Nếu dữ liệu có thì ta chuyển đổi dữ liệu thu được về dạng List<Obj>
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
                return products;
            }
        }
        // Ghi lại danh sách SP vào session
        public static void SetObjToSession(ISession session, string key, object data)
        {
            // Chuyển đổi dữ liệu ban đầu về dạng chuỗi Json để ghi vào session
            var jsonData = JsonConvert.SerializeObject(data);
            session.SetString(key, jsonData);   
        }
        // Kiểm tra xem SP có nằm trong 1 List hay không
        public static bool CheckObjInList(Guid id, List<Product> products)
        {
            return products.Any(p => p.Id == id);
            // Trả về true nếu điều kiện thỏa mãn và list không rỗng, false nếu ngược lại
        }
    }
}
