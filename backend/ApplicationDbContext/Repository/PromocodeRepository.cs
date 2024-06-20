using DbLevel.Data;
using DbLevel.Interface;
using DbLevel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DbLevel.Repository
{
    public class PromocodeRepository : IPromoCodeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PromocodeRepository(ApplicationDbContext context, ILogger logger = null)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateAsync(PromoCode promoCode)
        {
            await _context.PromoCodes.AddAsync(promoCode);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PromoCode code)
        {
            _context.PromoCodes.Remove(code);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PromoCode>> GetAllAsync()
        {
            return await _context.PromoCodes.ToListAsync();
        }

        public async Task<PromoCode> GetAsync(string code)
        {
            return await _context.PromoCodes.FirstOrDefaultAsync(x => x.Code == code) ?? throw new Exception();
        }

        public async Task UpdateAsync(PromoCode promoCode)
        {
            _context.PromoCodes.Update(promoCode);
            await _context.SaveChangesAsync();
        }
        public async Task<PromoCode> FindByIdAsync(string id)
        {
            return await _context.PromoCodes.FindAsync(id) ?? throw new Exception();
        }
    }
}
