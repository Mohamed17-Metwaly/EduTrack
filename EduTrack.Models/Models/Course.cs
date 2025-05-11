using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTrack.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string code { get; set; }
        [ForeignKey("course")]
        public int? PrerequisiteID { get; set; }
        public Course course { get; set; }

        public int CreditHours { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


    }
}
