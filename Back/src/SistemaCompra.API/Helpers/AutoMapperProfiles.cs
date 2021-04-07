using System.Linq;
using AutoMapper;

using SistemaCompra.API.DTO;
using SistemaCompra.API.Model;

namespace SistemaCompra.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
       
             CreateMap<Usuario, UsuarioDto>().ReverseMap();
           CreateMap<Usuario, UsuarioLoginDto>().ReverseMap();
        }
    }
}