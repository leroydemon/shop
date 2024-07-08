using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;
using System.Linq.Expressions;

namespace DbLevel.Specifications
{
    public class BrandSpecification : SpecificationBase<Brand>
    {
        public BrandSpecification(BrandFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                ApplyFilter(b => b.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Collection))
            {
                ApplyFilter(b => b.Collection.Contains(filter.Collection));
            }

            if (!string.IsNullOrEmpty(filter.Model))
            {
                ApplyFilter(b => b.Model.Contains(filter.Model));
            }


            ApplySorting(filter.SortBy, filter.Ascending);
            ApplyPaging(filter.Skip, filter.Take);
        }

        private void ApplySorting(BrandSortableFields sortBy, bool ascending)
        {
            Expression<Func<Brand, object>> orderByExpression = sortBy switch
            {
                BrandSortableFields.Name => b => b.Name,
                BrandSortableFields.Collection => b => b.Collection,
                BrandSortableFields.Model => b => b.Model,
                _ => b => b.Id
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