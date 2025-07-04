﻿using AutoMapper;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _unitOfWork.UserRepository.GetAll();

                return Ok(_mapper.Map<IEnumerable<UserGetDTO>>(users));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var user = _unitOfWork.UserRepository.Get(id);
                if (user == null) return NotFound();
                return Ok(_mapper.Map<UserGetDTO>(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserPostDTO dto)
        {
            try
            {
                var user = _mapper.Map<User>(dto);
                _unitOfWork.UserRepository.Add(user);
                _unitOfWork.Complete();
                // Fetch the newly created Jit with User relationship
                var createdUser = _unitOfWork.UserRepository.Get(user.UserId);

                return CreatedAtAction(nameof(Get), new { id = createdUser.UserId }, _mapper.Map<UserGetDTO>(createdUser));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserPutDTO dto)
        {
            try
            {
                var user = _unitOfWork.UserRepository.Get(dto.UserId);
                if (user == null) return NotFound();
                _mapper.Map(dto, user);
                _unitOfWork.Complete();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var user = _unitOfWork.UserRepository.Get(id);
                if (user == null) return NotFound();

                // delete all associagted jits

                var jits = _unitOfWork.JitRepository.GetAll()
                    .Where(j => j.UserId == user.UserId)
                    .ToList();

                if (jits.Any())
                {
                    foreach (var j in jits)
                        _unitOfWork.JitRepository.Remove(j);

                    _unitOfWork.Complete();
                }
                
 
                _unitOfWork.UserRepository.Remove(user);
                _unitOfWork.Complete();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
