using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.Models;

namespace EduTrack.DataAccess
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly EduTrackContext _context;
        public CourseRepository(EduTrackContext context) :base(context)
        {
            _context = context;
        }
        public async Task UpdataAsync(Course course)
        {
            var existingCourse = await _context.Courses.FindAsync(course.Id);
            if (existingCourse != null)
            {
                _context.Entry(existingCourse).CurrentValues.SetValues(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}
