using DbLevel.Data;
using DbLevel.Interfaces;
using DbLevel.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DbLevel.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> AddAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(e => e.Email == email);

            return user;
        }
        public async Task<User> GetByIdAsync(Guid userId)
        {
            var stringId = userId.ToString();

            var user = await _context.Users
                .FirstOrDefaultAsync(e => e.Id == stringId);

            return user;
        }

        public async Task<List<User>> GetSortedAsync(string searchTerm, int pageNumber, int pageSize, string sortBy, bool ascending)
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
        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task SetOnlineAsync(User user)
        {
            user.IsOnline = true;
            await _context.SaveChangesAsync();
        }
        public async Task SetOfflineAsync(User user)
        {
            user.IsOnline = false;
            await _context.SaveChangesAsync();
        }
    }
}
