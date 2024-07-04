using DbLevel.Interfaces;
using System.Linq.Expressions;

namespace DbLevel.Specifications
{
    public class CombinedSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; }

        public CombinedSpecification(ISpecification<T> spec1, ISpecification<T> spec2)
        {
            if (spec1.Criteria != null && spec2.Criteria != null)
            {
                Criteria = spec1.Criteria.AndAlso(spec2.Criteria);
            }
            else if (spec1.Criteria != null)
            {
                Criteria = spec1.Criteria;
            }
            else
            {
                Criteria = spec2.Criteria;
            }
            OrderBy = query =>
            {
                if (spec1.OrderBy != null && spec2.OrderBy != null)
                {
                    return spec2.OrderBy(spec1.OrderBy(query));
                }
                if (spec1.OrderBy != null)
                {
                    return spec1.OrderBy(query);
                }
                if (spec2.OrderBy != null)
                {
                    return spec2.OrderBy(query);
                }
                return (IOrderedQueryable<T>)query;
            };
        }
    }
}
