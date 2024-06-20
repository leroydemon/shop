using DbLevel.Models;

namespace DbLevel.Interface
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task<int> DeleteAsync(int id);
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
