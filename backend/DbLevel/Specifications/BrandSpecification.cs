using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;

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
            switch (sortBy)
            {
                case BrandSortableFields.Name:
                    if (ascending)
                        ApplyOrderBy(b => b.Name);
                    else
                        ApplyOrderByDescending(b => b.Name);
                    break;
                case BrandSortableFields.Collection:
                    if (ascending)
                        ApplyOrderBy(b => b.Collection);
                    else
                        ApplyOrderByDescending(b => b.Collection);
                    break;
                case BrandSortableFields.Model:
                    if (ascending)
                        ApplyOrderBy(b => b.Model);
                    else
                        ApplyOrderByDescending(b => b.Model);
                    break;
                default:
                    if (ascending)
                        ApplyOrderBy(b => b.Id);
                    else
                        ApplyOrderByDescending(b => b.Id);
                    break;
            }
        }
    }
}