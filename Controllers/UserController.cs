using AutoMapper;
using JitAPI.Auth;
using JitAPI.Models;
using JitAPI.Models.DTOS;
using JitAPI.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JitAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IUserProfileService _userProfileService;
        public UserController(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService, IUserProfileService userProfileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
            _userProfileService = userProfileService;
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery)) return BadRequest("Empty search term.");

            var results = _userProfileService.SearchUserProfiles(searchQuery);
            return Ok(results);
        }

        [HttpGet("profile/{username}")]
        public IActionResult GetUserProfile(string username)
        {
            var profile = _userProfileService.GetUserProfile(username);
            if (profile == null) 
                return NotFound();
            
            return Ok(profile);
        }

        [HttpGet("profile/")]
        public IActionResult GetUserProfile()
        {
            string? userId = HttpContext.GetUserId();
            Guid.TryParse(userId, out Guid guidUserId);
            var profile = _userProfileService.GetUserProfile(guidUserId);
            if(profile == null)
                return NotFound();
            return Ok(profile);
        }

        [HttpPost("profile")]
        public IActionResult UpdateUserProfile([FromBody] UserProfileUpdateDTO profileUpdate)
        {
            string? userId = HttpContext.GetUserId();
            Guid.TryParse(userId, out Guid guidUserId);
            var profileEntity = _userProfileService.GetUserProfile(guidUserId);
            if(profileEntity == null)
                return NotFound();
            bool success = _userProfileService.UpdateUserProfile(profileUpdate, guidUserId);
            return success ? Ok() : Conflict();
        }

    }
}
