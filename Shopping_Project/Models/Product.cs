namespace Shopping_Project.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int AvailableQuantity { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public virtual List<BillDetail> BillDetail { get; set; }
        public virtual List<CartDetail> CartDetail { get; set; }

    }
}
