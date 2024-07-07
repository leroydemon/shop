using DbLevel.SortableFields;

namespace DbLevel.Filters
{
    public class UserFilter
    {
        public string UserName {  get; set; }
        public string Email { get; set; }
        public bool IsOnline { get; set; }
        public UserSortableFields SortBy { get; set; }
        public bool Ascending {  get; set; } = true;
        public int Skip {  get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
