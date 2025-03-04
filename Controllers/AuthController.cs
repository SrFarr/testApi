using AuthAPI.Models;
using AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(IConfiguration config)
        {
            _authService = new AuthService();
            _tokenService = new TokenService(config);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (_authService.Register(user))
                return Ok(new { message = "Registrasi berhasil!" });

            return BadRequest(new { message = "Username sudah digunakan!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (_authService.Login(user.Username, user.Password))
            {
                var token = _tokenService.GenerateToken(user.Username);
                return Ok(new { message = "Login berhasil!", token });
            }

            return Unauthorized(new { message = "Username atau password salah!" });
        }
    }
}
