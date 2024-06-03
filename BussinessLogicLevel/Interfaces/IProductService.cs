
using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetProductById(int id);
        void Add(Product product);
        void Update(int id, Product product);
        void Delete(int id);
    }
}
