using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Configurations
{
    public class ProductRatingConfiguration : IEntityTypeConfiguration<ProductRating>
    {
        public void Configure(EntityTypeBuilder<ProductRating> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.RateValue)
                .IsRequired()
                .HasPrecision(3, 2);

            builder.Property(r => r.Count)
                .IsRequired();

            // Ensure unique rate value per product
            builder.HasIndex(r => new { r.ProductId, r.RateValue })
                .IsUnique();
        }
    }
} 