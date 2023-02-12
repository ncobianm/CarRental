using CarRental.Application.Interfaces.Repositories;
using CarRental.Infrastructure.Data.Data;

namespace CarRental.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public ICarRepository CarRepository => new CarRepository(_context);
        public ICarTypeRepository CarTypeRepository => new CarTypeRepository(_context);
        public ICarTypePriceRepository CarTypePriceRepository => new CarTypePriceRepository(_context);
        public ICustomerRepository CustomerRepository => new CustomerRepository(_context);
        public IRentingRepository RentingRepository => new RentingRepository(_context);

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
