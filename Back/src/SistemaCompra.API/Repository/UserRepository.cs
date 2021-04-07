using System.Collections.Generic;
using System.Linq;
using SistemaCompra.API.Model;

namespace SistemaCompra.API
{
    public static class UserRepository
    {
        public static Usuario Get(string username, string password)
        {
            var users = new List<Usuario>();
            users.Add(new Usuario{ Id = 1, Username = "batman", Password = "batman", Role = "Usuario" });
            users.Add(new Usuario { Id = 2, Username = "robin", Password = "robin", Role = "Fornecedor" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}