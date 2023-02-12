using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configuration
{
    public class CarTypeConfig : IEntityTypeConfiguration<CarType>
    {
        public void Configure(EntityTypeBuilder<CarType> builder)
        {
            builder.ToTable("CarTypes");

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.BasePrice)
                .IsRequired();

            builder.Property(x => x.LoyaltyPoints)
                .IsRequired();

            builder.HasMany(x => x.Cars)
                .WithOne(x => x.CarType)
                .HasForeignKey(x => x.CarTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CarTypePrices)
                .WithOne(x => x.CarType)
                .HasForeignKey(x => x.CarTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
