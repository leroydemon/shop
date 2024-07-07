using DbLevel.Filters;
using DbLevel.Models;

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
            switch (sortBy)
            {
                case UserSortableFields.UserName:
                    if (ascending)
                        ApplyOrderBy(u => u.UserName);
                    else
                        ApplyOrderByDescending(u => u.UserName);
                    break;
                case UserSortableFields.Id:
                    if (ascending)
                        ApplyOrderBy(u => u.Id);
                    else
                        ApplyOrderByDescending(u => u.Id);
                    break;
                case UserSortableFields.Email:
                    if (ascending)
                        ApplyOrderBy(u => u.Email);
                    else
                        ApplyOrderByDescending(u => u.Email);
                    break;
                case UserSortableFields.IsOnline:
                    if (ascending)
                        ApplyOrderBy(u => u.IsOnline);
                    else
                        ApplyOrderByDescending(u => u.IsOnline);
                    break;
                case UserSortableFields.CreateDateTime:
                    if (ascending)
                        ApplyOrderBy(u => u.CreatedDateTime);
                    else
                        ApplyOrderByDescending(u => u.CreatedDateTime);
                    break;
                case UserSortableFields.UpdateDateTime:
                    if (ascending)
                        ApplyOrderBy(u => u.UpdatedDateTime);
                    else
                        ApplyOrderByDescending(u => u.UpdatedDateTime);
                    break;
                default:
                    if (ascending)
                        ApplyOrderBy(u => u.Id);
                    else
                        ApplyOrderByDescending(u => u.Id);
                    break;
            }
        }
    }
}