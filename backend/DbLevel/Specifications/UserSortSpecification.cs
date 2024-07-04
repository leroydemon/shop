using DbLevel.Models;
using DbLevel.SortByEnum;

namespace DbLevel.Specifications
{
    public class UserSortSpecification : BaseSpecification<User>
    {
        public UserSortSpecification(UserSortBy sortBy, bool ascending)
        {
            OrderBy = query => ApplySorting(query, sortBy, ascending);
        }
        private IOrderedQueryable<User> ApplySorting(IQueryable<User> query, UserSortBy sortBy, bool ascending)
        {
            switch (sortBy)
            {
                case UserSortBy.UserName:
                    return ascending ? query.OrderBy(u => u.UserName) : query.OrderByDescending(u => u.UserName);
                case UserSortBy.Id:
                    return ascending ? query.OrderBy(u => u.Id) : query.OrderByDescending(u => u.Id);
                case UserSortBy.Email:
                    return ascending ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email);
                case UserSortBy.IsOnline:
                    return ascending ? query.OrderBy(u => u.IsOnline) : query.OrderByDescending(u => u.IsOnline);
                case UserSortBy.CreateDateTime:
                    return ascending ? query.OrderBy(u => u.CreatedDateTime) : query.OrderByDescending(u => u.CreatedDateTime);
                case UserSortBy.UpdateDateTime:
                    return ascending ? query.OrderBy(u => u.UpdatedDateTime) : query.OrderByDescending(u => u.UpdatedDateTime);
                default:
                    return query.OrderBy(u => u.Id);
            }
        }
    }

}