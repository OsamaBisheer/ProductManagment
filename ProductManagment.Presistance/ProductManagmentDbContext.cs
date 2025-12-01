using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Entities.Identity;
using ProductManagment.Domain.Interfaces.ICore;

namespace ProductManagment.Persistence
{
    public class ProductManagmentDbContext : IdentityDbContext<User, ApplicationRole, string, IdentityUserClaim<string>,
    ApplicationUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>, IProductManagmentDbContext
    {
        public ProductManagmentDbContext(DbContextOptions<ProductManagmentDbContext> options) : base(options)
        { }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}