using System.ComponentModel.DataAnnotations;

namespace ShopWebApi
{
    public class Category
    {
        public int id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Description { get; set; }

    }
}
