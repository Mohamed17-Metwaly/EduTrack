using System.ComponentModel.DataAnnotations;

namespace EduTrack.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        //public int FacultyId { get; set; }
        //// Navigation property
        //public Faculty Faculty { get; set; }
        // Collection of courses in the department
        List<Course> Courses { get; set; } = new List<Course>();
        List<Student> Students { get; set; } = new List<Student>();
    }
}
