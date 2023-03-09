
namespace Shopping_Project.Models
{
    public class BillDetail
    {
        public Guid Id { get; set; }
        public Guid IdHD { get; set; }
        public Guid IdSP { get; set; }
        public int Quantity { get; set; } 
        public int Price { get; set;}
        public virtual Product Product { get; set; }
        public virtual BIll BiLL { get; set; }
    }
}
