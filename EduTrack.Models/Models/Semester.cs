using System.ComponentModel.DataAnnotations;

namespace EduTrack.Models
{
    public class Semester
    {
        [Key]
        public int Id { get; set; }
        public string SemesterName { get; set; } // e.g., "Fall 2023"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
