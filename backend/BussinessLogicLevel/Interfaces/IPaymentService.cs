using DbLevel.Models;

namespace BussinessLogicLevel.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ConfirmPurchaseAsync(Guid cartId);
        Task<IEnumerable<PostOffice>> GetNearbyPostOffice(Guid userId, int maxResults);
    }
}
