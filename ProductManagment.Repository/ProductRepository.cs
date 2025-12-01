using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.ICore;
using ProductManagment.Domain.Interfaces.IRepositories;
using ProductManagment.Domain.ViewModels.Common;
using ProductManagment.Repository.Common;
using ProductManagment.Repository.Helpers;

namespace ProductManagment.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IProductManagmentDbContext context) : base(context)
        {
        }

        public async Task<DataTableResponseVM<Product>> Search(DataTableRequestVM requestVM)
        {
            var search = string.IsNullOrEmpty(requestVM.Search) ? string.Empty : requestVM.Search.ToLower();

            var result = GetAll().OrderByDescending(w => w.Id).Where(w => !w.IsDeleted)
                .Where(w => w.Id.ToString() == search || w.Name.ToLower().Contains(search))
                .Select(w => new Product
                {
                    Id = w.Id,
                    Name = w.Name,
                    Description = w.Description,
                    Price = w.Price
                });

            int totalRecords = result.Count();

            result = result.OrderByDynamic(requestVM.OrderColumn, requestVM.OrderDir)
                .Skip(requestVM.PageNumber * requestVM.PageSize)
                .Take(requestVM.PageSize);

            return await result.ToDataTableResult(totalRecords);
        }
    }
}