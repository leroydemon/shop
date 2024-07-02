namespace DbLevel.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public DateTime OrderDate { get; set; }
        public User User { get; set; }
    }
}