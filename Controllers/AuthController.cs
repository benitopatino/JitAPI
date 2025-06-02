using AutoMapper;
using JitAPI.Auth;
using JitAPI.Models;
using JitAPI.Models.Auth;
using JitAPI.Models.DTOS;
using JitAPI.Models.Services;
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
        private readonly UserRegistrationService _registrationService;

        public AuthController(IAuthService authService, UserRegistrationService registrationService)
        {
            _authService = authService;
            _registrationService = registrationService;
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDTO register)
        {
            
            bool created = _registrationService.RegisterUser(register);
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
