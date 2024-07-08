using DbLevel.Filters;
using DbLevel.Models;
using System.Linq.Expressions;

namespace DbLevel.Specifications
{
    public class UserSpecification : SpecificationBase<User>
    {
        public UserSpecification(UserFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                ApplyFilter(u => u.UserName.Contains(filter.UserName));
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                ApplyFilter(u => u.Email.Contains(filter.Email));
            }

            ApplyFilter(u => u.IsOnline == filter.IsOnline);
            ApplySorting(filter.SortBy, filter.Ascending);
            ApplyPaging(filter.Skip, filter.Take);
        }

        private void ApplySorting(UserSortableFields sortBy, bool ascending)
        {
            Expression<Func<User, object>> orderByExpression = sortBy switch
            {
                UserSortableFields.UserName => u => u.UserName,
                UserSortableFields.Email => u => u.Email,
                UserSortableFields.IsOnline => u => u.IsOnline,
                UserSortableFields.CreateDateTime => u => u.CreatedDateTime,
                UserSortableFields.UpdateDateTime => u => u.UpdatedDateTime,
                _ => u => u.Id
            };

            if (ascending)
            {
                ApplyOrderBy(orderByExpression);
            }
            else
            {
                ApplyOrderByDescending(orderByExpression);
            }
        }
    }
}