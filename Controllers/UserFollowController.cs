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
        public IActionResult Follow(Guid followeeId)
        {
            try
            {
                // Determine if follower exists

                string? userId = HttpContext.GetUserId();
                
                if(!Guid.TryParse(userId, out Guid userIdGuid) || !_unitOfWork.UserRepository.Exists(userIdGuid))
                    return BadRequest();

                // attempt to validate followee
                var followee = _unitOfWork.UserRepository.Get(followeeId);
                if (followee == null)
                    return NotFound(followeeId);

                    // create the UserFollow
                UserFollow userFollow = new UserFollow();
                userFollow.UserFollowerId = userIdGuid;
                userFollow.UserFolloweeId = followeeId;
                _unitOfWork.UserFollowRepository.Add(userFollow);

                _unitOfWork.Complete();
                return CreatedAtAction(nameof(Follow), new { id = userFollow.Id }, userFollow);
                
                return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("unfollow/{followeeId:guid}")]
        public IActionResult Unfollow(Guid followeeId)
        {
            try
            {
                // Determine if follower exists

                string? loggedInUserId = HttpContext.GetUserId();
                
                if(!Guid.TryParse(loggedInUserId, out Guid loggedInUserGuid) || !_unitOfWork.UserRepository.Exists(loggedInUserGuid))
                    return BadRequest();

                // attempt to validate followee
                var followee = _unitOfWork.UserRepository.Get(followeeId);
                if (followee == null)
                    return NotFound(followeeId);

                // create the UserFollow
                var follow = _unitOfWork.UserFollowRepository.GetAll()
                    .FirstOrDefault(f => f.UserFolloweeId == followeeId && f.UserFollowerId == loggedInUserGuid);

                if (follow == null)
                    return NotFound();
                
                _unitOfWork.UserFollowRepository.Remove(follow);
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
