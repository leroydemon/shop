using System.Linq.Expressions;

namespace DbLevel.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; }
    }
}
