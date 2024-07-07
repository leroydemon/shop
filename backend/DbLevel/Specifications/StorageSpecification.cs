using DbLevel.Filters;
using DbLevel.Models;
using DbLevel.SortableFields;

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
            switch (sortBy)
            {
                case StorageSortableFields.Name:
                    if (ascending)
                        ApplyOrderBy(u => u.Name);
                    else
                        ApplyOrderByDescending(u => u.Name);
                    break;
                case StorageSortableFields.Id:
                    if (ascending)
                        ApplyOrderBy(u => u.Id);
                    else
                        ApplyOrderByDescending(u => u.Id);
                    break;
                case StorageSortableFields.City:
                    if (ascending)
                        ApplyOrderBy(u => u.City);
                    else
                        ApplyOrderByDescending(u => u.City);
                    break;
                case StorageSortableFields.Phone:
                    if (ascending)
                        ApplyOrderBy(u => u.Phone);
                    else
                        ApplyOrderByDescending(u => u.Phone);
                    break;
                case StorageSortableFields.Address:
                    if (ascending)
                        ApplyOrderBy(u => u.Address);
                    else
                        ApplyOrderByDescending(u => u.Address);
                    break;
                case StorageSortableFields.CreateDateTime:
                    if (ascending)
                        ApplyOrderBy(u => u.CreatedDateTime);
                    else
                        ApplyOrderByDescending(u => u.CreatedDateTime);
                    break;
                case StorageSortableFields.UpdateDateTime:
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