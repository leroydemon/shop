namespace DbLevel.Models
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Collection { get; set; }
        public string Model { get; set; }
    }
}
