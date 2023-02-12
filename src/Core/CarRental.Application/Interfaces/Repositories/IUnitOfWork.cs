namespace CarRental.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        ICarRepository CarRepository { get; }
        ICarTypeRepository CarTypeRepository { get; }
        ICarTypePriceRepository CarTypePriceRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IRentingRepository RentingRepository { get; }

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        bool HasChanges();
    }
}
