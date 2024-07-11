using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ConfirmPurchaseAsync(Guid cartId);
        Task<IEnumerable<PostOffice>> GetNearestPostOfficesAsync(Guid userId, int maxResults);
    }
}
