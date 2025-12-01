using ProductManagment.API.Providers;
using ProductManagment.Domain.Interfaces.ICore;
using ProductManagment.Domain.Interfaces.IRepositories;
using ProductManagment.Persistence;
using ProductManagment.Repository.Common;
using ProductManagment.Service;
using ProductManagment.Domain.Interfaces.IServices;
using ProductManagment.API.JWT;
using ProductManagment.Repository;

namespace ProductManagment.API.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDIs(this IServiceCollection services)
        {
            services.AddScoped<IProductManagmentDbContext, ProductManagmentDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIdentityProvider, IdentityProvider>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddHttpClient<ProductService>();

            services.AddScoped<RevokableJwtSecurityTokenHandler>();
            services.AddScoped<JwtHandlerEvents>();

            return services;
        }
    }
}