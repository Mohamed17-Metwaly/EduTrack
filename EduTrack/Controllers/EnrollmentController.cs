using EduTrack.DataAccess;
using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Controllers
{
    [Route("api/Enrollment")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {

        private readonly IEnrollmentRepository _enrollmentRepository;
        public EnrollmentController(IEnrollmentRepository repo)
        {
            _enrollmentRepository=repo;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEnrollments()
        {
            try
            {
                var enrollment=await _enrollmentRepository.GetAllAsync();
                if(enrollment == null) 
                    return NotFound();
                APIResponse response = new APIResponse
                {
                    Status = "Success",
                    IsSuccess = true,
                    Data = enrollment
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                APIResponse response = new APIResponse
                {
                    Status = "Error",
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEnrollmentById(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();
                var enrollment = await _enrollmentRepository.GetByIdAsync(id);
                if (enrollment == null)
                    return NotFound();
                APIResponse response = new APIResponse
                {
                    Status = "Success",
                    IsSuccess = true,
                    Data = enrollment
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                APIResponse response = new APIResponse
                {
                    Status = "Error",
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEnrollment([FromBody] Enrollment enrollment)
        {
            try
            {
                if (enrollment == null)
                    return BadRequest();
                await _enrollmentRepository.AddAsync(enrollment);
                APIResponse response = new APIResponse
                {
                    Status = "Success",
                    IsSuccess = true,
                    Data = enrollment
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                APIResponse response = new APIResponse
                {
                    Status = "Error",
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEnrollment(int id, [FromBody] Enrollment enrollment)
        {
            if (id != enrollment.Id || enrollment.Id != id)
                return BadRequest();
            await _enrollmentRepository.UpdateAsync(enrollment);
            return Ok("Enrollment updated successfully");
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            if (id == 0)
                return BadRequest();
            if (id < 0)
                return BadRequest();
            var semester = await _enrollmentRepository.GetByIdAsync(id);
            if (semester == null)
                return NotFound();
            await _enrollmentRepository.DeleteAsync(id);
            return Ok(" Enrollment deleted successfully");

        }
    }
}
