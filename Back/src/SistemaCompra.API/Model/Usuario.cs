using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SistemaCompra.API.Model
{
    public class Usuario : IdentityUser<int>
    {
       
        public string fullName { get; set; }

          public List<UserRole> UserRoles{ get; set; }
 
      

      
    }
}