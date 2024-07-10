
namespace DbLevel.Models
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        // для чего здесь это свойство? строку из базы сразу нужно конвертировать в словарь
        public string? ProductListJson { get; set; }
        public Dictionary<Guid, int> ProductList { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductAmount { get; set; }
    }
}
