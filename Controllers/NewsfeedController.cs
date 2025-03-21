using AutoMapper;
using JitAPI.Auth;
using JitAPI.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JitAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class NewsfeedController : ControllerBase
    {
        private readonly INewsfeedService _newsfeedService;
        public NewsfeedController(INewsfeedService newsfeedService)
        {
            _newsfeedService = newsfeedService;
        }
        
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                string? userId = HttpContext.GetUserId();
                Guid.TryParse(userId, out Guid guidUserId);
                var newsfeed = _newsfeedService.GetNewsfeed(guidUserId);
                return Ok(newsfeed);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
