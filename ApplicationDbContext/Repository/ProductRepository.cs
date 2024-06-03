using DbLevel.Data;
using DbLevel.Interface;
using DbLevel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLevel.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = _context.Products.Find(id);

            _context.Products.Remove(item);
            await _context.SaveChangesAsync();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public async Task Update(int id, Product product)
        {
            var item = _context.Products.Find(id);

            item.Name = product.Name;
            item.Description = product.Description;
            item.Price = product.Price;
            item.Quantity = product.Quantity;
            await _context.SaveChangesAsync();
        }
    }
}
