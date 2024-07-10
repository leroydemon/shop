using DbLevel.Data;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.SortByEnum;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            var user = await _context.Users
                .FirstOrDefaultAsync(e => e.Id == userId);

            return user;
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
        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // эти два метода можно вынести в сервис и делать обычный апдейт

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

        public Task<List<User>> GetSortedAsync(string searchTerm, int pageNumber, int pageSize, UserSortBy sortBy, bool ascending)
        {
            throw new NotImplementedException();
        }

        //в обычном репозщитории есть такой метод, его отсюда удалить, вынести два предыдущих метода и этот юзе репозиторий можно будет удалить

        public async Task<IEnumerable<User>> ListAsync(ISpecification<User> spec, int pageNumber, int pageSize)
        {
            IQueryable<User> query = _context.Set<User>();

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy != null)
            {
                query = spec.OrderBy(query);
            }

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }
    }
}
