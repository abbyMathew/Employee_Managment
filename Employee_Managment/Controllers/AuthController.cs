using Microsoft.AspNetCore.Mvc;
using Employee_Managment.Services.Interfaces;
using Employee_Managment.Models;

namespace Employee_Managment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequest request)
        {
            if (request.UserName != "admin" || request.Password != "admin123")// Replace with your own authentication logic
            {
                return Unauthorized("Invalid username or password.");
            }

            var user = new User
            {
                UserName = request.UserName,
                Password = request.Password,
                Role = "Admin"
            };

            var token = _jwtService.GenerateToken(user);// Generate JWT token for the authenticated user
            return Ok(new { Token = token });
        }
    }
}
