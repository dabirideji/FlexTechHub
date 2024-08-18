using InventoryManagement.Application.Interfaces.GenericRepositoryPattern;
using InventoryManagement.Infrastructure.Data;
namespace InventoryManagement.Infrastructure.Repositories.GenericRepositoryImplementations
{
    public class UnitOfWork : IUnitOfWork
{
    private readonly InventoryManagementDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

    public UnitOfWork(InventoryManagementDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> GetRepository<T>() where T : class
    {
        if (!_repositories.ContainsKey(typeof(T)))
        {
            var repository = new GenericRepository<T>(_context);
            _repositories[typeof(T)] = repository;
        }

        return (IGenericRepository<T>)_repositories[typeof(T)];
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}


}