using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository repo)
        {
            _courseRepository = repo;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCourses()
        {
            try
            {
                var students = await _courseRepository.GetAllAsync();
                if (students == null)
                    return NotFound();
                APIResponse response = new APIResponse
                {
                    Status = "Success",
                    IsSuccess = true,
                    Data = students
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
        public async Task<IActionResult> GetCourseById(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();
                var student = await _courseRepository.GetByIdAsync(id);
                if (student == null)
                    return NotFound("Course not found");
                APIResponse response = new APIResponse
                {
                    Status = "Success",
                    IsSuccess = false,
                    Data = student
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            if (course == null)
                return BadRequest("Course cannot be null");
            await _courseRepository.AddAsync(course);
            return Ok("Course added successfully");
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            if (course == null || course.Id != id)
                return BadRequest("Invalid Course data");
            var existingStudent = await _courseRepository.GetByIdAsync(id);
            if (existingStudent == null)
                return NotFound("Course not found");
            await _courseRepository.UpdataAsync(course);
            return Ok("Course updated successfully");
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (id == 0)
                return BadRequest();
            if (id < 0)
                return BadRequest();
            var student = await _courseRepository.GetByIdAsync(id);
            if (student == null)
                return NotFound("Course not found");
            await _courseRepository.DeleteAsync(id);
            return Ok("Course deleted successfully");
        }
    }
}
