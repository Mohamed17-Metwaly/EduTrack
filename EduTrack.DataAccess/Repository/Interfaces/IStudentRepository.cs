using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.DTO;
using EduTrack.Models;
using EduTrack.Models.DTO;

namespace EduTrack.DataAccess.Repository.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task UpdateAsync(Student student);
        Task<List<CourseDTO>> GetCoursesAsync(int id);
        Task CourseRegistration(CourseRegistrationDTO registration);
        Task<List<CourseDTO>> GetAvailableCourses(int id);
        
        //Task<IEnumerable<Student>> GetStudentsByDepartmentIdAsync(int departmentId);
        //Task<IEnumerable<Student>> GetStudentsBySemesterIdAsync(int semesterId);
        //Task<IEnumerable<Student>> GetStudentsByCourseIdAsync(int courseId);
        //Task<Student> GetStudentByEnrollmentIdAsync(int enrollmentId);
        //Task<Student> GetStudentByCourseAndDepartmentAsync(int courseId, int departmentId);
    }
}
