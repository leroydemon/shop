namespace DbLevel.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        //public Guid CartId { get; set; } per User
        public DateTime OrderDate { get; set; }
    }
}
