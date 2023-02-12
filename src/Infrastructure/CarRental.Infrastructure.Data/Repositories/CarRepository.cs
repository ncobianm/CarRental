using CarRental.Application.Interfaces.Repositories;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;

        public CarRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Car entity)
        {
            _context.Cars.Add(entity);
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetByIdAsync(Guid id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public void Remove(Car entity)
        {
            _context.Cars.Remove(entity);
        }
    }
}
