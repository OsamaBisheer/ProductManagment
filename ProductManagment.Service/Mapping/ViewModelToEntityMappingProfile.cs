using AutoMapper;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.ViewModels.Product;

namespace ProductManagment.Service.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<ProductCreateVM, Product>().AfterMap((vm, entity) => entity.SetCreated(entity.CreatedByUserId, DateTime.UtcNow));
            CreateMap<ProductUpdateVM, Product>().AfterMap((vm, entity) => entity.SetLastUpdated(entity.LastUpdatedByUserId, DateTime.UtcNow));
        }
    }
}