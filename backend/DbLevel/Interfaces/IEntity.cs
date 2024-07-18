
namespace DbLevel.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreatedDateTime { get; }
        DateTime? UpdatedDateTime { get; }
    }
}
