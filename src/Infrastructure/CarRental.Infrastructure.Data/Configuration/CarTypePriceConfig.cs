using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configuration
{
    public class CarTypePriceConfig : IEntityTypeConfiguration<CarTypePrice>
    {
        public void Configure(EntityTypeBuilder<CarTypePrice> builder)
        {
            builder.ToTable("CarTypePrices");

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasOne(x => x.CarType)
                .WithMany(x => x.CarTypePrices)
                .HasForeignKey(x => x.CarTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
