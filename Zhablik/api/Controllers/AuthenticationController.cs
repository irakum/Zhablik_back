using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Zhablik.Models;
using Zhablik.Managers;

namespace Zhablik.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowLocalhost")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationManager _authenticationManager;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(AuthenticationManager authenticationManager, 
        ILogger<AuthenticationController> logger)
        {
            _authenticationManager = authenticationManager;
            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            try
            {
                var user = _authenticationManager.Register(request.Username, request.Email, request.Password);
                if (user == null)
                {
                    return Conflict("Username already exists.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering a user: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing the request. Please try again later.");
            }
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