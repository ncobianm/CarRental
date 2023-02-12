using CarRental.Domain.Entities;

namespace CarRental.Application.Interfaces.Repositories
{
    public interface ICarTypePriceRepository : IGenericRepository<CarTypePrice>
    {
        Task<IEnumerable<CarTypePrice>> GetAllByCarAsync(Guid carId);
    }
}
