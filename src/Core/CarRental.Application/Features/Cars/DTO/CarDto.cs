namespace CarRental.Application.Features.Cars.DTO
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public Guid CarTypeId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
    }
}
