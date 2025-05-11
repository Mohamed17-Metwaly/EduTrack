using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.Models;
using EduTrack.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using EduTrack.DataAccess;

namespace EduTrack.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository repo)
        {
            _studentRepository = repo;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var students = await _studentRepository.GetAllAsync();
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
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();
                var student = await _studentRepository.GetByIdAsync(id);
                if (student == null)
                    return NotFound("Student not found");
                APIResponse response = new APIResponse
                {
                    Status = "Success",
                    IsSuccess = false,
                    Data = student
                };
                return Ok(response);
            }
            catch(Exception ex) 
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
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (student == null)
                return BadRequest("Student cannot be null");
            await _studentRepository.AddAsync(student);
            return Ok("Student added successfully");
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student student)
        {
            if (student == null || student.Id != id)
                return BadRequest("Invalid student data");
            var existingStudent = await _studentRepository.GetByIdAsync(id);
            if (existingStudent == null)
                return NotFound("Student not found");
            await _studentRepository.UpdateAsync(student);
            return Ok("Student updated successfully");
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (id == 0)
                return BadRequest();
            if (id < 0)
                return BadRequest();
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                return NotFound("Student not found");
            await _studentRepository.DeleteAsync(id);
            return Ok("Student deleted successfully");
        }
        [HttpGet("course")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudentCourse(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Student ID cannot be zero");
                if (id < 0)
                    return BadRequest("Student ID cannot be negative");
                var course = await _studentRepository.GetCoursesAsync(id);
                if (course == null || course.Count == 0)
                    return NotFound("No courses found for this student");
                APIResponse response = new APIResponse
                {
                    Status = "Success",
                    IsSuccess = true,
                    Data = course
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
        [HttpGet("AvailableCourse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAvailableCourses(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Student ID cannot be zero");
                if (id < 0)
                    return BadRequest("Student ID cannot be negative");
                var course = await _studentRepository.GetAvailableCourses(id);
                if (course == null || course.Count == 0)
                    return NotFound("No courses found for this student");
                APIResponse response = new APIResponse
                {
                    Status = "Success",
                    IsSuccess = true,
                    Data = course
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
        [HttpPost("courseRegistration")]
        public async Task<IActionResult> Registration([FromBody] CourseRegistrationDTO courseRegistrationDTO)
        {
            await _studentRepository.CourseRegistration(courseRegistrationDTO);
            return Ok("done");
        }
    }
}