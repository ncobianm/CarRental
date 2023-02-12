namespace CarRental.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void Remove(T entity);
    }
}
