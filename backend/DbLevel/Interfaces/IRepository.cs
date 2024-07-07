﻿namespace DbLevel.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
        Task<IEnumerable<T>> ListAsync(ISpecification<T> spec);
    }
}
