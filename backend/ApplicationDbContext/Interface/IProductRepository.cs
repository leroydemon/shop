

using DbLevel.Models;

namespace DbLevel.Interface
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task DeleteAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateAsync(int id, Product product);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
