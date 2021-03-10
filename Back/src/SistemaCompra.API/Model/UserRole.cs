using Microsoft.AspNetCore.Identity;

namespace SistemaCompra.API.Model
{
    public class UserRole : IdentityUserRole<int>
    {
        public Usuario User { get; set; }
        public Roles Role { get; set; }
    }
}