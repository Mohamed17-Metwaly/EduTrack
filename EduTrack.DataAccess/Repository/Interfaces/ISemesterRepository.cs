using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.Models;

namespace EduTrack.DataAccess.Repository.Interfaces
{
    public interface ISemesterRepository : IRepository<Semester>
    {
        Task UpdateAsync(Semester semester);
        //Task<IEnumerable<Semester>> GetSemestersByDepartmentIdAsync(int departmentId);
        //Task<IEnumerable<Semester>> GetSemestersByCourseIdAsync(int courseId);
        //Task<IEnumerable<Semester>> GetSemestersByStudentIdAsync(int studentId);
        //Task<Semester> GetSemesterByCourseAndDepartmentAsync(int courseId, int departmentId);
    }
}
