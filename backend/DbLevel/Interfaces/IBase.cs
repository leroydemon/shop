
namespace DbLevel.Interfaces
{
    public interface IBase
    {
        Guid Id { get; }
        DateTime CreatedDateTime { get; }
        DateTime? UpdatedDateTime { get; }
    }
}
