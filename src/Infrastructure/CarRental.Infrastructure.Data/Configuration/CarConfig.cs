using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configuration
{
    public class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Brand)
                .IsRequired();

            builder.Property(x => x.Model)
                .IsRequired();

            builder.Property(x => x.PlateNumber)
                .IsRequired();

            builder.HasOne(x => x.CarType)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.CarTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Renting)
                .WithOne(x => x.Car)
                .HasForeignKey(x => x.CarId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
