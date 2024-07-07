using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;

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

            ApplySorting(filter.SortBy, filter.Ascending);
            ApplyPaging(filter.Skip, filter.Take);
        }

        private void ApplySorting(CategorySortableFields sortBy, bool ascending)
        {
            switch (sortBy)
            {
                case CategorySortableFields.Name:
                    if (ascending)
                        ApplyOrderBy(c => c.Name);
                    else
                        ApplyOrderByDescending(c => c.Name);
                    break;
                case CategorySortableFields.Id:
                    if (ascending)
                        ApplyOrderBy(c => c.Id);
                    else
                        ApplyOrderByDescending(c => c.Id);
                    break;
                case CategorySortableFields.CreateDateTime:
                    if (ascending)
                        ApplyOrderBy(c => c.CreatedDateTime);
                    else
                        ApplyOrderByDescending(c => c.CreatedDateTime);
                    break;
                case CategorySortableFields.UpdateDateTime:
                    if (ascending)
                        ApplyOrderBy(c => c.UpdatedDateTime);
                    else
                        ApplyOrderByDescending(c => c.UpdatedDateTime);
                    break;
                default:
                    if (ascending)
                        ApplyOrderBy(c => c.Id);
                    else
                        ApplyOrderByDescending(c => c.Id);
                    break;
            }
        }
    }
}