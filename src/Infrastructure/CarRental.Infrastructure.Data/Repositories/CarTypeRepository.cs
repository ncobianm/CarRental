using CarRental.Application.Interfaces.Repositories;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Data.Repositories
{
    public class CarTypeRepository : ICarTypeRepository
    {
        private readonly DataContext _context;

        public CarTypeRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(CarType entity)
        {
            _context.CarTypes.Add(entity);
        }

        public async Task<IEnumerable<CarType>> GetAllAsync()
        {
            return await _context.CarTypes.ToListAsync();
        }

        public async Task<CarType> GetByIdAsync(Guid id)
        {
            return await _context.CarTypes.FindAsync(id);
        }

        public void Remove(CarType entity)
        {
            _context.CarTypes.Remove(entity);
        }
    }
}
