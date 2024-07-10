using System.Linq.Expressions;
using DbLevel.Interfaces;

namespace DbLevel.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; protected set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; protected set; }
    }
}
