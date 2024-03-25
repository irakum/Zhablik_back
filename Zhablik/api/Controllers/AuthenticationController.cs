using Microsoft.AspNetCore.Mvc;
using Zhablik.Models;
using Zhablik.Managers;

namespace Zhablik.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationManager _authenticationManager;

        public AuthenticationController(AuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var user = _authenticationManager.Register(request.Username, request.Email, request.Password);
            if (user == null)
            {
                return Conflict("Username already exists.");
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _authenticationManager.Login(request.Username, request.Password);
            if (user == null)
            {
                return NotFound("User not found or incorrect password.");
            }
            
            return Ok(user.UserID);
        }
    }
}