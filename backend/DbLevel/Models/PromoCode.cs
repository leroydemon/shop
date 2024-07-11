using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class PromoCode : EntityBase
    {
        public string? Code { get; set; }
        public int AmountDiscoint { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; }
    }
}
