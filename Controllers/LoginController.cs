using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Datos;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public LoginController(AppDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Logins
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Nickname == loginDto.Username);

            if (user == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            if (user.Password != loginDto.Password)
            {
                return Unauthorized("Contraseña incorrecta.");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Nickname),
                    new Claim(ClaimTypes.Role, user.Rol?.descripcion ?? "Usuario")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],      
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });
        }
    }
}
