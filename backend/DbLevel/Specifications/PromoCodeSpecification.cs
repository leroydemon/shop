using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;

namespace DbLevel.Specifications
{
    public class PromoCodeSpecification : SpecificationBase<PromoCode>
    {
        public PromoCodeSpecification(PromoCodeFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Code))
            {
                ApplyFilter(p => p.Code.Contains(filter.Code));
            }

            if (!string.IsNullOrEmpty(filter.Code))
            {
                ApplyFilter(p => p.Code.Contains(filter.Code));
            }

            ApplyFilter(p => p.AmountDiscoint == filter.AmountDiscount);
            ApplyFilter(p => p.ExpireDate == filter.ExpireDate);
            ApplyFilter(p => p.IsActive == filter.IsActive);

            ApplySorting(filter.SortBy, filter.Ascending);
            ApplyPaging(filter.Skip, filter.Take);
        }

        private void ApplySorting(PromoCodeSortableFields sortBy, bool ascending)
        {
            switch (sortBy)
            {
                case PromoCodeSortableFields.Code:
                    if (ascending)
                        ApplyOrderBy(p => p.Code);
                    else
                        ApplyOrderByDescending(p => p.Code);
                    break;
                case PromoCodeSortableFields.Id:
                    if (ascending)
                        ApplyOrderBy(p => p.Id);
                    else
                        ApplyOrderByDescending(p => p.Id);
                    break;
                case PromoCodeSortableFields.AmountDiscoint:
                    if (ascending)
                        ApplyOrderBy(p => p.AmountDiscoint);
                    else
                        ApplyOrderByDescending(p => p.AmountDiscoint);
                    break;
                case PromoCodeSortableFields.ExpireDate:
                    if (ascending)
                        ApplyOrderBy(p => p.ExpireDate);
                    else
                        ApplyOrderByDescending(p => p.ExpireDate);
                    break;
                case PromoCodeSortableFields.CreateDateTime:
                    if (ascending)
                        ApplyOrderBy(p => p.CreatedDateTime);
                    else
                        ApplyOrderByDescending(p => p.CreatedDateTime);
                    break;
                case PromoCodeSortableFields.UpdateDateTime:
                    if (ascending)
                        ApplyOrderBy(p => p.UpdatedDateTime);
                    else
                        ApplyOrderByDescending(p => p.UpdatedDateTime);
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