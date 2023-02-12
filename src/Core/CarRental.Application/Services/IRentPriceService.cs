using CarRental.Application.Interfaces.Repositories;
using CarRental.Domain.Entities;
using MediatR;

namespace CarRental.Application.Services
{
    public interface IRentPriceService
    {
        Task<decimal> GetRentPrice(Guid carId, int daysNumber);
        Task<decimal> GetSurcharges(Guid rentingId, DateTime endDate);
    }

    public class RentPriceService : IRentPriceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentPriceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal> GetRentPrice(Guid carId, int daysNumber)
        {
            decimal totalRentPrice = 0;

            var car = await _unitOfWork.CarRepository.GetByIdAsync(carId);
            var carType = await _unitOfWork.CarTypeRepository.GetByIdAsync(car.CarTypeId);
            var carTypePrices = await _unitOfWork.CarTypePriceRepository.GetAllByCarAsync(carId);
            var rentPrices = carTypePrices.Where(x => x.MinDay == null || x.MinDay < daysNumber).OrderBy(x => x.MinDay);

            if (rentPrices.Any())
            {
                foreach (var rentPrice in rentPrices)
                {
                    var daysInRange = GetIntersectDaysInRange(rentPrice, daysNumber);
                    totalRentPrice += rentPrice.Price * daysInRange;
                }
            }
            else
            {
                totalRentPrice = carType.BasePrice * daysNumber;
            }

            return totalRentPrice;
        }

        public async Task<decimal> GetSurcharges(Guid rentingId, DateTime endDate)
        {
            var renting = await _unitOfWork.RentingRepository.GetByIdAsync(rentingId);

            if (endDate.Date < renting.EndDate)
                return 0;

            var numberOfDays = (int)(endDate.Date - renting.EndDate.Date).TotalDays;
            var car = await _unitOfWork.CarRepository.GetByIdAsync(renting.CarId);
            var carType = await _unitOfWork.CarTypeRepository.GetByIdAsync(car.CarTypeId);

            return carType.ExtraDayPrice * numberOfDays;
        }

        private int GetIntersectDaysInRange(CarTypePrice rentPrice, int daysNumber)
        {
            var minDays = rentPrice.MinDay ?? 0;
            var maxDays = rentPrice.MaxDay == null ? daysNumber : Math.Min(daysNumber, rentPrice.MaxDay.Value);
            var daysInRange = maxDays - minDays;

            return daysInRange;
        }
    }
}
