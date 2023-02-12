namespace CarRental.Domain.Entities
{
    public class CarTypePrice : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CarTypeId { get; set; }
        public CarType CarType { get; set; }
        public int? MinDay { get; set; }
        public int? MaxDay { get; set; }
        public decimal Price { get; set; }
    }
}
