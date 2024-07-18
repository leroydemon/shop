using DbLevel.Models;

namespace DbLevel.Specifications
{
    public class ProductStorageByProductIdsSpecification : SpecificationBase<ProductStorage>
    {
        public ProductStorageByProductIdsSpecification(IEnumerable<Guid> productIds)
        {
            ApplyFilter(ps => productIds.Contains(ps.ProductId));
        }
    }
}
