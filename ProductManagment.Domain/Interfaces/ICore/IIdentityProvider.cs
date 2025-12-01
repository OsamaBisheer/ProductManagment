using ProductManagment.Domain.Entities.Identity;

namespace ProductManagment.Domain.Interfaces.ICore
{
    public interface IIdentityProvider
    {
        User GetUser();
    }
}