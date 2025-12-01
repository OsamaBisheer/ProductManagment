using ProductManagment.Domain.ViewModels.Common;
using ProductManagment.Domain.ViewModels.Product;
using static ProductManagment.Domain.Enums.Enumeration;

namespace ProductManagment.Domain.Interfaces.IServices
{
    public interface IProductService
    {
        Task<Tuple<int, ResponseCodeEnum>> Create(ProductCreateVM workflowVM);

        Task<Tuple<int, ResponseCodeEnum>> Update(ProductUpdateVM workflowVM);

        Task<Tuple<DataTableResponseVM<ProductResultVM>, ResponseCodeEnum>> Search(DataTableRequestVM workflowVM);

        Task<Tuple<ProductResultVM, ResponseCodeEnum>> GetById(int id);

        Task<Tuple<int, ResponseCodeEnum>> GetCount();
        Task<ResponseCodeEnum> Delete(int id, string lastUpdatedByUserId);
    }
}