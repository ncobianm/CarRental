using CarRental.Application.Interfaces.Repositories;
using CarRental.Domain.Entities;
using CarRental.Infrastructure.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Data.Repositories
{
    public class RentingRepository : IRentingRepository
    {
        private readonly DataContext _context;

        public RentingRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Renting entity)
        {
            _context.Renting.Add(entity);
        }

        public async Task<IEnumerable<Renting>> GetAllAsync()
        {
            return await _context.Renting.ToListAsync();
        }

        public async Task<Renting> GetByIdAsync(Guid id)
        {
            return await _context.Renting.FindAsync(id);
        }

        public void Remove(Renting entity)
        {
            _context.Renting.Remove(entity);
        }
    }
}
