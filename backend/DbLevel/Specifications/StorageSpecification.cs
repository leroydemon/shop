using DbLevel.Enum;
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
                ApplyFilter(u => u.AddressInfo.Address.Contains(filter.Address));
            }

            if (!string.IsNullOrEmpty(filter.City))
            {
                ApplyFilter(u => u.AddressInfo.City.Contains(filter.City));
            }

            if (!string.IsNullOrEmpty(filter.Phone))
            {
                ApplyFilter(u => u.Phone.Contains(filter.Phone));
            }

            ApplySorting(filter.OrderBy, filter.Ascending);
            ApplyPaging(filter.Skip, filter.Take);
        }

        private void ApplySorting(StorageSortableFields sortBy, OrderByDirection ascending)
        {
            Expression<Func<Storage, object>> orderByExpression = sortBy switch
            {
                StorageSortableFields.Name => u => u.Name,
                StorageSortableFields.City => u => u.AddressInfo.City,
                StorageSortableFields.Phone => u => u.Phone,
                StorageSortableFields.Address => u => u.AddressInfo.Address,
                StorageSortableFields.CreateDateTime => u => u.CreatedDateTime,
                StorageSortableFields.UpdateDateTime => u => u.UpdatedDateTime,
                _ => u => u.Id
            };

            if (ascending == OrderByDirection.Ascending)
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