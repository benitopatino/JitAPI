using JitAPI.Auth;
using JitAPI.Models;
using JitAPI.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthRegister register)
        {
            User user = new User();
            user.Email = register.Email;
            user.FirstName = register.FirstName;
            user.LastName = register.LastName;

            bool created = _authService.Register(user, register.Password);
            if (created)
                return Ok(new { message = "User registered successfully" });
            
            return BadRequest(new { message = "Unable to register user" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthLogin loginForm)
        {
            var authResult = _authService.Authenticate(loginForm.Username, loginForm.Password);
            return authResult.IsAuthenticated ? Ok(authResult) : Unauthorized(authResult);

        }
    }
}
