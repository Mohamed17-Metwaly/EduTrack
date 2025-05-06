using Microsoft.EntityFrameworkCore;

namespace EduTrack.Models
{
    public class EduTrackContext : DbContext
    {
        public EduTrackContext(DbContextOptions<EduTrackContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Semester> semesters { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
