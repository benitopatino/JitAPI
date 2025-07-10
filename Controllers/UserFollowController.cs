using System.Security.Claims;
using AutoMapper;
using JitAPI.Auth;
using JitAPI.Models.DTOS;
using JitAPI.Models.Follows;
using JitAPI.Models.Interface;
using JitAPI.Models.Services;
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
        private readonly IUserProfileService _profileService;

        public UserFollowController(IUnitOfWork unitOfWork, IUserProfileService profileService)
        {
            _unitOfWork = unitOfWork;
            _profileService = profileService;
        }

        [HttpGet("following")]
        public IActionResult GetFollowing()
        {
            string? userId = HttpContext.GetUserId();
                
            if(!Guid.TryParse(userId, out Guid userIdGuid) || !_unitOfWork.UserRepository.Exists(userIdGuid))
                return BadRequest();

            return Ok(_profileService.GetFollowees(userIdGuid));
        }

        [HttpPost("follow/{followeeUsername}")]
        public IActionResult Follow(string followeeUsername)
        {
            try
            {
                // Determine if follower exists

                string? userId = HttpContext.GetUserId();
                
                if(!Guid.TryParse(userId, out Guid userIdGuid) || !_unitOfWork.UserRepository.Exists(userIdGuid))
                    return BadRequest();

                // attempt to validate followee
                var followee = _unitOfWork.UserRepository.GetAll()
                    .SingleOrDefault(f => f.Username == followeeUsername);
                if (followee == null)
                    return NotFound(followeeUsername);

                    // create the UserFollow
                UserFollow userFollow = new UserFollow();
                userFollow.UserFollowerId = userIdGuid;
                userFollow.UserFolloweeId = followee.UserId;
                _unitOfWork.UserFollowRepository.Add(userFollow);
                
               
                _profileService.UpdateFolloweeCount(userIdGuid, UpdateAction.Increase); // The logged in user will update its followee count
                _profileService.UpdateFollowersCount(followee.UserId, UpdateAction.Increase); // The user we will follow needs to update its follower count
                
                
                _unitOfWork.Complete();
                return CreatedAtAction(nameof(Follow), new { id = userFollow.Id }, userFollow);
                
                return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("unfollow/{followeeUsername}")]
        public IActionResult Unfollow(string followeeUsername)
        {
            try
            {
                // Determine if follower exists

                string? loggedInUserId = HttpContext.GetUserId();
                
                if(!Guid.TryParse(loggedInUserId, out Guid loggedInUserGuid) || !_unitOfWork.UserRepository.Exists(loggedInUserGuid))
                    return BadRequest();

                // attempt to validate followee
                var followee = _unitOfWork.UserRepository.GetAll()
                    .SingleOrDefault(f => f.Username == followeeUsername);
                if (followee == null)
                    return NotFound(followeeUsername);
                
                var follow = _unitOfWork.UserFollowRepository.GetAll()
                    .FirstOrDefault(f => f.UserFolloweeId == followee.UserId && f.UserFollowerId == loggedInUserGuid);

                if (follow == null)
                    return NotFound();
                
                _unitOfWork.UserFollowRepository.Remove(follow);
                
                _profileService.UpdateFolloweeCount(loggedInUserGuid, UpdateAction.Decrease); // The logged in user will update its followee count
                _profileService.UpdateFollowersCount(followee.UserId, UpdateAction.Decrease); // The user we will follow needs to update its follower count

                _unitOfWork.Complete();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
