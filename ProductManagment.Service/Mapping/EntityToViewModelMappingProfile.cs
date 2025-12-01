using AutoMapper;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.ViewModels.Common;
using ProductManagment.Domain.ViewModels.Product;

namespace ProductManagment.Service.Mappings
{
    public class EntityToViewModelMappingProfile : Profile
    {
        public EntityToViewModelMappingProfile()
        {
            CreateMap<Product, LookupVM>();
            CreateMap<Product, ProductResultVM>();
            CreateMap<DataTableResponseVM<Product>, DataTableResponseVM<ProductResultVM>>();
        }
    }
}