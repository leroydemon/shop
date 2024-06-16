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

        public async Task AddAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _productRepository.GetProductByIdAsync(id);

            await _productRepository.DeleteAsync(item);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task UpdateAsync(int id, Product product)
        {
            await _productRepository.UpdateAsync(id, product);
        }
    }
}
