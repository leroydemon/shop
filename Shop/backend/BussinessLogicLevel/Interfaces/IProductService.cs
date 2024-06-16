namespace BussinessLogicLevel.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> AddAsync(ProductDto product);
        Task<ProductDto> UpdateAsync(int id, ProductDto product);
        Task<int> DeleteAsync(int id);
    }
}
