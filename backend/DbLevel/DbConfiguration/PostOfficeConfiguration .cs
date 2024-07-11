﻿using DbLevel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DbLevel.DbConfiguration
{
    public class PostOfficeConfiguration : IEntityTypeConfiguration<PostOffice>
    {
        public void Configure(EntityTypeBuilder<PostOffice> builder)
        {
            builder
                .OwnsOne(u => u.AddressInfo, a =>
                {
                    a.Property(p => p.Country).HasColumnName("Country");
                    a.Property(p => p.City).HasColumnName("City");
                    a.Property(p => p.Address).HasColumnName("Address");
                });
        }
    }
}
