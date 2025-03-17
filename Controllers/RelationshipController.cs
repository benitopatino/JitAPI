using System.Security.Claims;
using AutoMapper;
using JitAPI.Models.Interface;
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
        public IActionResult Follow(string followeeId)
        {
            // extract the claims User ID 
            var userID = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            return StatusCode(200);
        }


    }
}
