
namespace BussinessLogicLevel.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ConfirmPurchaseAsync(Guid cartId, int quantity);
    }
}
