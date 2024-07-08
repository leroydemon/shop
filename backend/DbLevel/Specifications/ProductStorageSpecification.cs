using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;
using System.Linq.Expressions;

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
            Expression<Func<ProductStorage, object>> orderByExpression = sortBy switch
            {
                ProductStorageSortableFields.StorageId => p => p.StorageId,
                ProductStorageSortableFields.ProductId => p => p.ProductId,
                ProductStorageSortableFields.Quantity => p => p.Quantity,
                _ => p => p.Id
            };

            if (ascending)
            {
                ApplyOrderBy(orderByExpression);
            }
            else
            {
                ApplyOrderByDescending(orderByExpression);
            }
        }
    }
}