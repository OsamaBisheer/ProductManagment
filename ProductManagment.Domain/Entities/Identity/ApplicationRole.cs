using Microsoft.AspNetCore.Identity;

namespace ProductManagment.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public List<ApplicationUserRole> UserRoles { get; set; }
    }
}