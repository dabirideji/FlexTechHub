namespace InventoryManagement.Application.Interfaces.GenericRepositoryPattern
{
    public interface IUnitOfWork : IDisposable
{   
    IGenericRepository<T> GetRepository<T>() where T : class;
    Task<int> SaveAsync();
    }



}