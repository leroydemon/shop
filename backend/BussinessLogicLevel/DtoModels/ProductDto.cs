using BussinessLogicLevel.DtoModels;

namespace Infrastucture.DtoModels
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public SizeDto Size { get; set; }
        public GenderDto Gender { get; set; }
        public SeasonDto Season { get; set; }
        public PurposeDto Purpose { get; set; }
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
    }
}
