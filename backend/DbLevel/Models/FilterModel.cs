using DbLevel.Enum;

namespace DbLevel.Models
{
    public class FilterModel
    {
        public string FieldName { get; set; }
        public ComparisonType Comparison { get; set; }
        public object FieldValue { get; set; }
    }
}
