using EduTrack.DataAccess;
using EduTrack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace EduTrack.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;
        public StudentController(StudentRepository repo)
        {
            _studentRepository = repo;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentRepository.GetAllAsync();
            if (students == null)
                return BadRequest();
            APIResponse response = new APIResponse
            {
                Status = "Success",
                IsSuccess = true,
                Data = students
            };

            return Ok(response);
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
            if(id<0)
                return BadRequest();
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                return NotFound("Student not found");
            await _studentRepository.DeleteAsync(id);
            return Ok("Student deleted successfully");
        }
    }
}
