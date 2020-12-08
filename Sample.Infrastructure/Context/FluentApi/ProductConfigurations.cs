using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities;

namespace Sample.Infrastructure.Context.FluentApi
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Photo).HasMaxLength(1024);

            entity.Property(e => e.Price)
                .IsRequired()
                .HasMaxLength(200);
        }
    }

}
