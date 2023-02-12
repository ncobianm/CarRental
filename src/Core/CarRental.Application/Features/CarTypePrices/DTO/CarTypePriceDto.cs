namespace CarRental.Application.Features.CarTypePrices.DTO
{
    public class CarTypePriceDto
    {
        public Guid Id { get; set; }
        public Guid CarTypeId { get; set; }
        public int? MinDay { get; set; }
        public int? MaxDay { get; set; }
        public decimal Price { get; set; }
    }
}
