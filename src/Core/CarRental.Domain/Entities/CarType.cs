namespace CarRental.Domain.Entities
{
    public class CarType : BaseEntity
    {
        public CarType()
        {
            Cars = new HashSet<Car>();
            CarTypePrices = new HashSet<CarTypePrice>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int LoyaltyPoints { get; set; }
        public decimal BasePrice { get; set; }
        public decimal ExtraDayPrice { get; set; }

        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<CarTypePrice> CarTypePrices { get; set; }
    }
}
