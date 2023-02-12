namespace CarRental.Domain.Entities
{
    public class Renting : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? RealEndDate { get; set; }
        public decimal Price { get; set; }
        public decimal? Surcharges { get; set; }
    }
}
