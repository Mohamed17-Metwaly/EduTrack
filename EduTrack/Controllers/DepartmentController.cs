using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Controllers
{
    [Route("api/Department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository repo)
        {
            _departmentRepository=repo;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepartment()
        {
            try
            {
                var students = await _departmentRepository.GetAllAsync();
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
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();
                var student = await _departmentRepository.GetByIdAsync(id);
                if (student == null)
                    return NotFound("Department not found");
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
        public async Task<IActionResult> AddDepartment([FromBody] Department Dept)
        {
            if (Dept == null)
                return BadRequest("Department cannot be null");
            await _departmentRepository.AddAsync(Dept);
            return Ok("Department added successfully");
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department Dept)
        {
            if (Dept == null || Dept.Id != id)
                return BadRequest("Invalid Department data");
            var existingStudent = await _departmentRepository.GetByIdAsync(id);
            if (existingStudent == null)
                return NotFound("Department not found");
            await _departmentRepository.UpdataAsync(Dept);
            return Ok("Department updated successfully");
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (id == 0)
                return BadRequest();
            if (id < 0)
                return BadRequest();
            var student = await _departmentRepository.GetByIdAsync(id);
            if (student == null)
                return NotFound("Department not found");
            await _departmentRepository.DeleteAsync(id);
            return Ok("Department deleted successfully");
        }

    }
}
