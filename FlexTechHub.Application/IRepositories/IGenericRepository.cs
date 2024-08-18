using InventoryManagement.Domain.Models;

namespace InventoryManagement.Application.Interfaces.GenericRepositoryPattern
{
    public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
}



}