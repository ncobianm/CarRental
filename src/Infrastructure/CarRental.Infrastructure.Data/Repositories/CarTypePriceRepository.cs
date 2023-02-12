using CarRental.Application.Interfaces.Repositories;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Data.Repositories
{
    public class CarTypePriceRepository : ICarTypePriceRepository
    {
        private readonly DataContext _context;

        public CarTypePriceRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(CarTypePrice entity)
        {
            _context.CarTypePrices.Add(entity);
        }

        public async Task<IEnumerable<CarTypePrice>> GetAllAsync()
        {
            return await _context.CarTypePrices.ToListAsync();
        }

        public async Task<IEnumerable<CarTypePrice>> GetAllByCarAsync(Guid carId)
        {
            var car = await _context.Cars.FindAsync(carId);

            return await _context.CarTypePrices
                .Where(x => x.CarTypeId == car.CarTypeId)
                .ToListAsync();
        }

        public async Task<CarTypePrice> GetByIdAsync(Guid id)
        {
            return await _context.CarTypePrices.FindAsync(id);
        }

        public void Remove(CarTypePrice entity)
        {
            _context.CarTypePrices.Remove(entity);
        }
    }
}
