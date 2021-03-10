using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SistemaCompra.API.Model
{
    public class Roles : IdentityRole<int>
    {
        public List<UserRole> UserRoles{ get; set; }
    }
}