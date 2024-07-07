using DbLevel.Interfaces;

namespace DbLevel.Models
{
    public class PromoCode : IEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int AmountDiscoint { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedDateTime { get; set; } = DateTime.Now;
    }
}
