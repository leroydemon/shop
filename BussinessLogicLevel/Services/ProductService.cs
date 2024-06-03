  using BussinessLogicLevel.Interfaces;
using DbLevel.Interface;
using DbLevel.Models;
using DbLevel.Repository;

namespace BussinessLogicLevel.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Add(Product product)
        {
            _productRepository.Add(product);
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public void Update(int id, Product product)
        {
            _productRepository.Update(id, product);
        }
    }
}
