using DbLevel.Data;
using DbLevel.Interface;
using DbLevel.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product item)
        {
            _context.Products.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task UpdateAsync(int id, Product product)
        {
            // move mapping to service level and call only update here
            var item = await _context.Products.FindAsync(id);

            item.Name = product.Name;
            item.Description = product.Description;
            item.UnitPrice = product.UnitPrice;
            await _context.SaveChangesAsync();
        }
    }
}
