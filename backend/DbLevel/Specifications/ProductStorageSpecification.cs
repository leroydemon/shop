using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;

namespace DbLevel.Specifications
{
    public class ProductStorageSpecification : SpecificationBase<ProductStorage>
    {
        public ProductStorageSpecification(ProductStorageFilter filter)
        {
            ApplyFilter(p => p.StorageId == filter.StorageId);
            ApplyFilter(p => p.ProductId == filter.ProductId);
            ApplyFilter(p => p.Quantity == filter.Quantity);

            ApplySorting(filter.SortBy, filter.Ascending);
            ApplyPaging(filter.Skip, filter.Take);
        }

        private void ApplySorting(ProductStorageSortableFields sortBy, bool ascending)
        {
            switch (sortBy)
            {
                case ProductStorageSortableFields.StorageId:
                    if (ascending)
                        ApplyOrderBy(p => p.StorageId);
                    else
                        ApplyOrderByDescending(p => p.StorageId);
                    break;
                case ProductStorageSortableFields.ProductId:
                    if (ascending)
                        ApplyOrderBy(p => p.ProductId);
                    else
                        ApplyOrderByDescending(p => p.ProductId);
                    break;
                case ProductStorageSortableFields.Quantity:
                    if (ascending)
                        ApplyOrderBy(p => p.Quantity);
                    else
                        ApplyOrderByDescending(p => p.Quantity);
                    break;
                default:
                    if (ascending)
                        ApplyOrderBy(p => p.Id);
                    else
                        ApplyOrderByDescending(p => p.Id);
                    break;
            }
        }
    }
}