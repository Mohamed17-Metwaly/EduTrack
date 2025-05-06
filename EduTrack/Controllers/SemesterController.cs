using EduTrack.DataAccess;
using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace EduTrack.Controllers
{
    [Route("api/Semester")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterRepository _semesterRepository;
        public SemesterController(ISemesterRepository repo)
        {
            _semesterRepository = repo;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSemesters()
        {
            try
            {
                var semesters = await _semesterRepository.GetAllAsync();
                if (semesters == null)
                    return NotFound();
                APIResponse response = new APIResponse
                {
                    Status = "Success",
                    IsSuccess = true,
                    Data = semesters
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
        public async Task<IActionResult> GetSemesterById(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();
                var semester = await _semesterRepository.GetByIdAsync(id);
                if (semester == null)
                    return NotFound();
                APIResponse response = new APIResponse
                {
                    Status = "Success",
                    IsSuccess = true,
                    Data = semester
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
        public async Task<IActionResult> CreateSemester([FromBody] Semester semester)
        {
            try
            {
                if (semester == null)
                    return BadRequest();
                await _semesterRepository.AddAsync(semester);
                APIResponse response = new APIResponse
                {
                    Status = "Success",
                    IsSuccess = true,
                    Data = semester
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
        public async Task<IActionResult> UpdateSemester(int id, [FromBody] Semester semester)
        {
                if (id != semester.Id || semester.Id!=id)
                    return BadRequest();
                await _semesterRepository.UpdateAsync(semester);
                return Ok("Semester updated successfully");
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSemester(int id)
        {
            if(id==0)
                return BadRequest();
            if(id<0)
                return BadRequest();
            var semester= await _semesterRepository.GetByIdAsync(id);
            if (semester==null)
                return NotFound();
            await _semesterRepository.DeleteAsync(id);
            return Ok(" Semester deleted successfully");

        }
    }
}
