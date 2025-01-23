using JitAPI.Models.Interface;
using JitAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public JitController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_unitOfWork.JitRepository.GetAll().ToList());
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
                var jit = _unitOfWork.JitRepository.Get(id);
                if (jit == null) return NotFound();
                return Ok(jit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Jit jit)
        {
            try
            {
                _unitOfWork.JitRepository.Add(jit);
                _unitOfWork.Complete();
                return CreatedAtAction(nameof(Get), new { id = jit.Id }, jit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Jit updatedJit)
        {
            try
            {
                var jit = _unitOfWork.JitRepository.Get(id);
                if (jit == null) return NotFound();
                jit.Content = updatedJit.Content;
                jit.DateCreated = updatedJit.DateCreated;
                jit.UserId = updatedJit.UserId;
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
