
namespace DbLevel.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
