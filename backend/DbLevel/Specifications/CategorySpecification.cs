using DbLevel.Enum;
using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;
using System.Linq.Expressions;

namespace DbLevel.Specifications
{
    public class CategorySpecification : SpecificationBase<Category>
    {
        public CategorySpecification(CategoryFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                ApplyFilter(c => c.Name.Contains(filter.Name));
            }

            ApplySorting(filter.OrderBy, filter.Ascending);
            ApplyPaging(filter.Skip, filter.Take);
        }

        private void ApplySorting(CategorySortableFields sortBy, OrderByDirection ascending)
        {
            Expression<Func<Category, object>> orderByExpression = sortBy switch
            {
                CategorySortableFields.Name => c => c.Name,
                CategorySortableFields.CreateDateTime => c => c.CreatedDateTime,
                CategorySortableFields.UpdateDateTime => c => c.UpdatedDateTime,
                _ => c => c.Id
            };

            if (ascending == OrderByDirection.Ascending)
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