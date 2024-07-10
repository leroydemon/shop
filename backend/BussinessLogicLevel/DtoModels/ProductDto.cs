using DbLevel.Enum;

namespace Infrastucture.DtoModels
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid BrandId { get; set; }
        //для каждой енамки с уровня базы данных должная отвечать енамка с уровня бизнес логики - SizeDto и тд
        public Size Size { get; set; }
        public Gender Gender { get; set; }
        public Season Season { get; set; }
        public Purpose Propose { get; set; }
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
    }
}
