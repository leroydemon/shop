
using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddAsync(Product product);
        Task Update(Product product);
        Task DeleteAsync(int id);
    }
}
