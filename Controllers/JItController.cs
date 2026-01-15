using AutoMapper;
using JitAPI.Auth;
using JitAPI.Models.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JitAPI.Models;
using JitAPI.Models.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace JitAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class JitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public JitController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var jit = _unitOfWork.JitRepository.GetAll()
                    .Include(j => j.User)
                    .FirstOrDefault(j => j.Id == id);
                if (jit == null) return NotFound();
                return Ok(_mapper.Map<JitGetDTO>(jit));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] JitPostDTO dto)
        {
            try
            {
                var jit = _mapper.Map<Jit>(dto);
                
                // Extract UserId from jwt token
                
                string? userId = HttpContext.GetUserId();
                if (!Guid.TryParse(userId, out Guid outUserId))
                    throw new ArgumentNullException(nameof(outUserId), "Unable to extract UserId from token");
                
                jit.UserId = outUserId;
                _unitOfWork.JitRepository.Add(jit);
                _unitOfWork.Complete();
                // Fetch the newly created Jit with User relationship
                var createdJit = _unitOfWork.JitRepository.GetAll()
                    .Include(j => j.User)
                    .FirstOrDefault(j => j.Id == jit.Id);

                return CreatedAtAction(nameof(Get), new { id = createdJit.Id }, _mapper.Map<JitGetDTO>(createdJit));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] JitPutDTO dto)
        {
            try
            {
                var jit = _unitOfWork.JitRepository.Get(dto.Id);
                if (jit == null) return NotFound();
                _mapper.Map(dto, jit);
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
                var jit = _unitOfWork.JitRepository.Get(id);
                if (jit == null) return NotFound();
                _unitOfWork.JitRepository.Remove(jit);
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
