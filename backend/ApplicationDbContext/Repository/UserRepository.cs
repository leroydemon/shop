using DbLevel.Data;
using DbLevel.Interface;
using DbLevel.Models;
using Microsoft.EntityFrameworkCore;

namespace DbLevel.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(e => e.Email == email);

            return user;
        }
        public async Task<User> GetById(string userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(e => e.Id == userId);

            return user;
        }

        public async Task<List<User>> GetSortedUsersAsync(string searchTerm, int pageNumber, int pageSize, string sortBy, bool ascending)
        {
            IQueryable<User> query = _context
                .Users
                .Where(u => u.UserName.Contains(searchTerm) || u.Email.Contains(searchTerm)) 
                ?? throw new Exception();

            switch (sortBy.ToLower())
            {
                case "username":
                    query = ascending ? query.OrderBy(u => u.UserName) : query.OrderByDescending(u => u.UserName);
                    break;
                case "email":
                    query = ascending ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email);
                    break;
                default:
                    query = query.OrderBy(u => u.Id);
                    break;
            }
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
