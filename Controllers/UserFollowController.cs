using System.Security.Claims;
using AutoMapper;
using JitAPI.Auth;
using JitAPI.Models.DTOS;
using JitAPI.Models.Follows;
using JitAPI.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;

namespace JitAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserFollowController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserFollowController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("follow/{followeeId:guid}")]
        public IActionResult Follow(string followeeId)
        {

            try
            {
                // Determine if follower exists

                string? userId = HttpContext.GetUserId();
                if(!Guid.TryParse(userId, out Guid userIdGuid) || _unitOfWork.UserRepository.Get(userIdGuid) == null)
                    return BadRequest();


                // attempt to validate followee
                if (Guid.TryParse(followeeId, out Guid followeeIdGuid))
                {
                    var followee = _unitOfWork.UserRepository.Get(followeeIdGuid);
                    if (followee == null)
                        return NotFound(followeeIdGuid);

                    // create the UserFollow
                    UserFollow userFollow = new UserFollow();
                    userFollow.UserFollowerId = userIdGuid;
                    userFollow.UserFolloweeId = followeeIdGuid;
                    _unitOfWork.UserFollowRepository.Add(userFollow);

                    _unitOfWork.Complete();
                    return CreatedAtAction(nameof(Follow), new { id = userFollow.Id }, userFollow);

                }
                
                return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


    }
}
