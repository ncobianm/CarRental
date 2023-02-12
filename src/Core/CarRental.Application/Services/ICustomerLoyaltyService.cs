using CarRental.Application.Interfaces.Repositories;

namespace CarRental.Application.Services
{
    public interface ICustomerLoyaltyService
    {
        Task<int> GetCustomerLoyaltyPointsAsync(Guid customerId);
    }

    public class CustomerLoyaltyService : ICustomerLoyaltyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerLoyaltyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> GetCustomerLoyaltyPointsAsync(Guid customerId)
        {
            var loyaltyPoints = 0;

            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            var rentals = await _unitOfWork.RentingRepository.GetAllAsync();
            var customerRentals = rentals.Where(x => x.CustomerId == customer.Id);

            foreach (var rental in customerRentals)
            {
                var rentalCar = await _unitOfWork.CarRepository.GetByIdAsync(rental.CarId);
                var carType = await _unitOfWork.CarTypeRepository.GetByIdAsync(rentalCar.CarTypeId);
                loyaltyPoints += carType.LoyaltyPoints;
            }

            return loyaltyPoints;
        }
    }
}
