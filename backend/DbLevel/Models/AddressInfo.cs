
using Microsoft.EntityFrameworkCore;

namespace DbLevel.Models
{
    [Owned]
    public class AddressInfo
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
    }
}
