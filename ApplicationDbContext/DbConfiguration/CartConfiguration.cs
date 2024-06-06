
using DbLevel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbLevel.DbConfiguration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(u => u.UnitPrice).HasPrecision(18, 2);
            builder.Property(u => u.TotalPrice).HasPrecision(18, 2);
        }
    }
}
