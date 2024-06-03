

using DbLevel.Models;

namespace DbLevel.Interface
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task Delete(int id);
        Product GetProductById(int id);
        Task Update(int id, Product product);
        IEnumerable<Product> GetAll();
    }
}
