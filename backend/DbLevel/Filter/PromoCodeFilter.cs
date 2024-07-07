using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class PromoCodeFilter
    {
        public string Code { get; set; }
        public int AmountDiscount { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; }
        public PromoCodeSortableFields SortBy { get; set; }
        public bool Ascending {  get; set; } = true;
        public int Skip {  get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
