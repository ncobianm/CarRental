namespace CarRental.Application.Features.CarTypes.DTO
{
    public class CarTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int LoyaltyPoints { get; set; }
        public decimal BasePrice { get; set; }
        public decimal ExtraDayPrice { get; set; }
    }
}
