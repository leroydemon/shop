
using AutoMapper;
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
    }
}
