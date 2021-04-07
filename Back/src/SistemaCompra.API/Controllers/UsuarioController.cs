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
using Microsoft.AspNetCore.Authorization;
using SistemaCompra.API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace SistemaCompra.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> userManager;

        public IConfiguration _config { get; }
        public UserManager<Usuario> _userManager { get; }
        public SignInManager<Usuario> _signInManager { get; }
        public IMapper _mapper { get; }

        public UsuarioController(IConfiguration config,
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        IMapper mapper)
        {
          
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

    
        
         [HttpGet("GetUser")]
             [AllowAnonymous]
    public async Task<IActionResult> GetUser()
    {
        return Ok(new UserDto());
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        try
        {
            var user = _mapper.Map<Usuario>(userDto);

            var result = await _userManager.CreateAsync(user ,userDto.Password);

            var userToReturn = _mapper.Map<UserDto>(user);

            if (result.Succeeded) 
            {
                return Created("GetUser", userToReturn);
            }
            
            return BadRequest(result.Errors);
        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
        }
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginDto userLogin)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(userLogin.UserName);

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userLogin.UserName.ToUpper());

                var userToReturn = _mapper.Map<UserLoginDto>(appUser);

                return Ok(new {
                    token = GenerateJWToken(appUser).Result,
                    user = userToReturn
                });
            }

            return Unauthorized();
        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
        }
    }

    private async Task<string> GenerateJWToken(Usuario user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.ASCII
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      
         var tokenJwt = new SecurityTokenDescriptor
           {
               Subject = new ClaimsIdentity(claims),
               Expires = DateTime.UtcNow.AddMinutes(15),
               //Expires = DateTime.Now.AddMinutes(15),
               SigningCredentials = creds
           };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenJwt);

        return tokenHandler.WriteToken(token);
    }
  }
}


    