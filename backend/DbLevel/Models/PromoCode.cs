namespace DbLevel.Models
{
    public class PromoCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int AmountDiscoint { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; set; }
    }
}
