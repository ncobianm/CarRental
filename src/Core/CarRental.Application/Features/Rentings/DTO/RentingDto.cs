namespace CarRental.Application.Features.Rentings.DTO
{
    public class RentingDto
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? RealEndDate { get; set; }
        public decimal Price { get; set; }
        public decimal? Surcharges { get; set; }
    }
}
