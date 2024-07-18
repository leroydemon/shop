using BussinessLogicLevel.DtoModels;

namespace BussinessLogicLevel.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Register(RegisterDto request);
        Task<string> Login(LoginDto input);
        Task<bool> LogOut(Guid userId);
    }
}
