namespace CarRental.Domain.Entities
{
    public class Car : BaseEntity
    {
        public Car()
        {
            Renting = new HashSet<Renting>();
        }

        public Guid Id { get; set; }
        public Guid CarTypeId { get; set; }
        public CarType CarType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }

        public IEnumerable<Renting> Renting { get; set; }
    }
}
