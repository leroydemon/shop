using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;
using System.Linq.Expressions;

namespace DbLevel.Specifications
{
    public class ProductSpecification : SpecificationBase<Product>
    {
        public ProductSpecification(ProductFilter filter)
        {
            if (filter.CategoryId.HasValue)
            {
                ApplyFilter(p => p.CategoryId == filter.CategoryId.Value);
            }

            if (filter.BrandId.HasValue)
            {
                ApplyFilter(p => p.BrandId == filter.BrandId.Value);
            }

            if (filter.Size.HasValue)
            {
                ApplyFilter(p => p.Size == filter.Size.Value);
            }

            if (filter.Gender.HasValue)
            {
                ApplyFilter(p => p.Gender == filter.Gender.Value);
            }

            if (filter.Season.HasValue)
            {
                ApplyFilter(p => p.Season == filter.Season.Value);
            }

            if (filter.Purpose.HasValue)
            {
                ApplyFilter(p => p.Propose == filter.Purpose.Value);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                ApplyFilter(p => p.Name.Contains(filter.Name));
            }

            if (filter.MinPrice.HasValue)
            {
                ApplyFilter(p => p.UnitPrice >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                ApplyFilter(p => p.UnitPrice <= filter.MaxPrice.Value);
            }

            ApplySorting(filter.SortBy, filter.Ascending);
            ApplyPaging(filter.Skip, filter.Take);
        }
        private void ApplySorting(ProductSortableFields sortBy, bool ascending)
        {
            Expression<Func<Product, object>> orderByExpression = sortBy switch
            {
                ProductSortableFields.Name => p => p.Name,
                ProductSortableFields.UnitPrice => p => p.UnitPrice,
                ProductSortableFields.CreateDateTime => p => p.CreatedDateTime,
                ProductSortableFields.UpdateDateTime => p => p.UpdatedDateTime,
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
