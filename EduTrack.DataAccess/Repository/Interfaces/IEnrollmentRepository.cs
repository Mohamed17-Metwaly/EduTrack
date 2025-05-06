using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.Models;

namespace EduTrack.DataAccess.Repository.Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        Task UpdateAsync(Enrollment enrollment);
        //Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId);
        //Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId);
        //Task<IEnumerable<Enrollment>> GetEnrollmentsBySemesterIdAsync(int semesterId);
        //Task<Enrollment> GetEnrollmentByStudentAndCourseAsync(int studentId, int courseId);
    }
}
