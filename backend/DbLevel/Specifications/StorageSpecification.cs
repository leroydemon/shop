using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;
using System.Linq.Expressions;

namespace DbLevel.Specifications
{
    public class StorageSpecification : SpecificationBase<Storage>
    {
        public StorageSpecification(StorageFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                ApplyFilter(u => u.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Address))
            {
                ApplyFilter(u => u.Address.Contains(filter.Address));
            }

            if (!string.IsNullOrEmpty(filter.City))
            {
                ApplyFilter(u => u.City.Contains(filter.City));
            }

            if (!string.IsNullOrEmpty(filter.Phone))
            {
                ApplyFilter(u => u.Phone.Contains(filter.Phone));
            }

            ApplySorting(filter.SortBy, filter.Ascending);
            ApplyPaging(filter.Skip, filter.Take);
        }

        private void ApplySorting(StorageSortableFields sortBy, bool ascending)
        {
            Expression<Func<Storage, object>> orderByExpression = sortBy switch
            {
                StorageSortableFields.Name => u => u.Name,
                StorageSortableFields.City => u => u.City,
                StorageSortableFields.Phone => u => u.Phone,
                StorageSortableFields.Address => u => u.Address,
                StorageSortableFields.CreateDateTime => u => u.CreatedDateTime,
                StorageSortableFields.UpdateDateTime => u => u.UpdatedDateTime,
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