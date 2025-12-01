using ProductManagment.Domain.Entities;
using ProductManagment.Domain.ViewModels.Common;

namespace ProductManagment.Domain.Interfaces.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<DataTableResponseVM<Product>> Search(DataTableRequestVM workflowVM);
    }
}