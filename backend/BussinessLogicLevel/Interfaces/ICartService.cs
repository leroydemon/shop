using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetAsync(Guid cartId);
        Task<CartDto> AddToAsync(Guid cartId, Guid productId, int quantity);
        Task RemoveFromAsync(Guid cartId, Guid productId, int quantity);
        Task ClearAsync(Guid cartId);
    }
}
