using DbLevel.Enum;
using DbLevel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbLevel.DbConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(u => u.UnitPrice)
                   .HasPrecision(18, 2);

            builder.Property(s => s.Season)
                   .HasConversion(s => s.ToString(), s => (Season)Season
                   .Parse(typeof(Season), s));

            builder.Property(g => g.Gender)
                   .HasConversion(g => g.ToString(), g => (Gender)Gender
                   .Parse(typeof(Gender), g));

            builder.Property(p => p.Propose)
                   .HasConversion(p => p.ToString(), p => (Purpose)Purpose
                   .Parse(typeof(Purpose), p));

            builder.Property(p => p.Size)
                   .HasConversion(s => s.ToString(), s => (Size)Size
                   .Parse(typeof(Size), s));
        }
    }
}
