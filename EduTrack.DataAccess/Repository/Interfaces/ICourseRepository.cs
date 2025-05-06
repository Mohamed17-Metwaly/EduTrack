using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.Models;

namespace EduTrack.DataAccess.Repository.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task UpdataAsync(Course course);
        //Task<IEnumerable<Course>> GetCoursesByDepartmentIdAsync(int departmentId);
        //Task<IEnumerable<Course>> GetCoursesBySemesterIdAsync(int semesterId);
    }
}
