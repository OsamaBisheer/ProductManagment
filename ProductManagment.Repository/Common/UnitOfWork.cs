using ProductManagment.Domain.Interfaces.ICore;
using ProductManagment.Domain.Interfaces.IRepositories;

namespace ProductManagment.Repository.Common
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        public IProductManagmentDbContext Context { get; }
        public IProductRepository Products { get; private set; }

        public UnitOfWork(IProductManagmentDbContext _context)
        {
            Context = _context;

            Products = new ProductRepository(_context);
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public async Task<int> Commit()
        {
            // Save changes with the default options
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}