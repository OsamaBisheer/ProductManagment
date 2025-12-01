using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Domain.Interfaces.ICore
{
    public interface IProductManagmentDbContext : IDisposable
    {
        DbSet<Product> Products { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}