

using DbLevel.Data;
using DbLevel.Interface;
using DbLevel.Models;

namespace DbLevel.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public Category GetProductById(int id)
        {
            return _context.Categories.Find(id);
        }

        public async Task Update(int id, Category category)
        {
            var item = _context.Categories.Find(id);
            item.Name = category.Name;
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
      
    }
}
