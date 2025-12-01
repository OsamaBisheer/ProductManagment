using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.ICore;
using ProductManagment.Domain.Interfaces.IServices;
using ProductManagment.Domain.ViewModels.Common;
using ProductManagment.Domain.ViewModels.Product;
using static ProductManagment.Domain.Enums.Enumeration;

namespace ProductManagment.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<Tuple<DataTableResponseVM<ProductResultVM>, ResponseCodeEnum>> Search(DataTableRequestVM requestVM)
        {
            var dataTableResponse = await unitOfWork.Products.Search(requestVM);
            return Tuple.Create(mapper.Map<DataTableResponseVM<Product>, DataTableResponseVM<ProductResultVM>>(dataTableResponse), ResponseCodeEnum.Success);
        }

        public async Task<Tuple<int, ResponseCodeEnum>> Create(ProductCreateVM model)
        {
            var product = mapper.Map<ProductCreateVM, Product>(model);

            await unitOfWork.Products.Add(product);

            await unitOfWork.Commit();

            return Tuple.Create(product.Id, ResponseCodeEnum.Success);
        }

        public async Task<Tuple<int, ResponseCodeEnum>> Update(ProductUpdateVM model)
        {
            var productDB = await unitOfWork.Products.Get(p => p.Id == model.Id && !p.IsDeleted).AsNoTracking().FirstOrDefaultAsync();

            if (productDB == null || productDB.IsDeleted) return Tuple.Create(0, ResponseCodeEnum.NotFound);

            var product = mapper.Map<ProductUpdateVM, Product>(model);

            product.SetCreated(productDB.CreatedByUserId, productDB.CreatedOn);

            unitOfWork.Products.Update(product);

            await unitOfWork.Commit();

            return Tuple.Create(product.Id, ResponseCodeEnum.Success);
        }

        public async Task<Tuple<ProductResultVM, ResponseCodeEnum>> GetById(int id)
        {
            var product = await unitOfWork.Products.Find(id);

            if (product == null || product.IsDeleted) return Tuple.Create(new ProductResultVM(), ResponseCodeEnum.NotFound);

            var model = mapper.Map<Product, ProductResultVM>(product);

            return Tuple.Create(model, ResponseCodeEnum.Success);
        }

        public async Task<Tuple<int, ResponseCodeEnum>> GetCount()
        {
            var count = await unitOfWork.Products.Get(p => !p.IsDeleted).CountAsync();

            return Tuple.Create(count, ResponseCodeEnum.Success);
        }

        public async Task<ResponseCodeEnum> Delete(int id, string lastUpdatedByUserId)
        {
            var product = await unitOfWork.Products.Get(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            product.MarkAsDeleted(lastUpdatedByUserId);
            await unitOfWork.Commit();

            return ResponseCodeEnum.Success;
        }
    }
}