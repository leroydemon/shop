using DbLevel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DbLevel.DbConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.HistoryOrders)
                      .WithOne(o => o.User)
                      .HasForeignKey(o => o.CartId)
                      .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
