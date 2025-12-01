using Microsoft.AspNetCore.Identity;

namespace ProductManagment.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public List<ApplicationUserRole> UserRoles { get; set; }
    }
}