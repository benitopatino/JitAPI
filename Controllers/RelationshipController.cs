using System.Security.Claims;
using AutoMapper;
using JitAPI.Models.Interface;
using JitAPI.Models.Relationships;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace JitAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RelationshipController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RelationshipController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("follow/{followeeId:guid}")]
        public IActionResult Follow(Guid followeeId)
        {
            // extract the claims User ID 
            var userID = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrWhiteSpace(userID))
                return BadRequest();

            Relationship newRelationship = new Relationship();
            newRelationship.FollowerId = Guid.Parse(userID);
            newRelationship.FolloweeId = followeeId;
            _unitOfWork.Relationships.Add(newRelationship);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(Follow), new { id = newRelationship.Id }, newRelationship);
        }


    }
}
