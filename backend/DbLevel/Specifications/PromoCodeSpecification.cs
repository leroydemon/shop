using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;
using System.Linq.Expressions;

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
            Expression<Func<PromoCode, object>> orderByExpression = sortBy switch
            {
                PromoCodeSortableFields.Code => p => p.Code,
                PromoCodeSortableFields.AmountDiscoint => p => p.AmountDiscoint,
                PromoCodeSortableFields.ExpireDate => p => p.ExpireDate,
                PromoCodeSortableFields.CreateDateTime => p => p.CreatedDateTime,
                PromoCodeSortableFields.UpdateDateTime => p => p.UpdatedDateTime,
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