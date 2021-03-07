using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaCompra.API.Model;

namespace SistemaCompra.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
      

        public UsuarioController()
        {
            
        }

        [HttpGet]
        public string Get()
        {
            return "ValueTask";
        }

         [HttpGet("{id}")]
        public string GetById(int id)
        {
            return "ValueTask";
        }


    }
}
