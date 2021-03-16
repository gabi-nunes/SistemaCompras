using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SistemaCompra.API.Model;
using AutoMapper;

namespace SistemaCompra.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> userManager;

        public IConfiguration Config { get; }
        public UserManager<Usuario> UserManager { get; }
        public SignInManager<Usuario> SignInManager { get; }
        public IMapper Mapper { get; }

        public UsuarioController(IConfiguration config,
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        IMapper mapper)
        {
          
            this.Config = config;
            this.UserManager = userManager;
            SignInManager = signInManager;
            Mapper = mapper;
        }

        [HttpGet]
        public  async Task<IActionResult> GetUser()
        {
            return Ok(new Usuario());
        }

        [HttpGet("{id}")]
        public string GetById(int id)
        {
            return "ValueTask";
        }


    }
}
