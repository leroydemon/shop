using DbLevel.Filter;
using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class PromoCodeFilter : FilterBase<PromoCodeSortableFields>
    {
        public string? Code { get; set; }
        public int? AmountDiscount { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
