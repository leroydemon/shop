
namespace BussinessLogicLevel.Interfaces
{
    public interface IPostOfficeService
    {
        Task<string> GetPostOfficesJsonAsync();
    }
}
