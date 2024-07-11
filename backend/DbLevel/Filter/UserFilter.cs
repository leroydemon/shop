using DbLevel.Filter;

namespace DbLevel.Filters
{
    public class UserFilter : FilterBase<UserSortableFields>
    {
        public string? UserName {  get; set; }
        public string? Email { get; set; }
        public bool? IsOnline { get; set; }
    }
}
