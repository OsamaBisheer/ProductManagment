using ProductManagment.Domain.Interfaces.IRepositories;

namespace ProductManagment.Domain.Interfaces.ICore
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        IProductManagmentDbContext Context { get; }

        Task<int> Commit();
    }
}