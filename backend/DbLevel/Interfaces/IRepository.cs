namespace DbLevel.Interfaces
{
    public interface IRepository<T> where T : IBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
        Task<IEnumerable<T>> ListAsync(ISpecification<T> spec, int pageNumber, int pageSize);
    }
}
