using DbLevel.Models;

namespace DbLevel.Specifications
{
    public class UserSearchSpecification : BaseSpecification<User>
    {
        public UserSearchSpecification(string searchTerm)
        {
            var toLowerCaseSearch = searchTerm.ToLower();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                Criteria = u =>
                u.UserName.Contains(toLowerCaseSearch) ||
                u.Email.Contains(toLowerCaseSearch) ||
                u.Surname.Contains(toLowerCaseSearch);
            }
            else
            {
                Criteria = u => true;
            }
        }
    }
}
