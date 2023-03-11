namespace Shopping_Project.Models
{
    public class Cart
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
