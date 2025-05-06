using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTrack.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public decimal ?Grade { get; set; } // Grade can be a decimal or string depending on your grading system
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [ForeignKey("Semester")]
        public int SemesterId { get; set; }
        public Student Student { get; set; } // Navigation property to Student
        public Semester Semester { get; set; } // Navigation property to Semester
        public Course Course { get; set; } // Navigation property to Course

        //public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        // Navigation properties
        //public virtual Student Student { get; set; }
        //public virtual Course Course { get; set; }
        //public virtual Semester Semester { get; set; }
        // Additional properties can be added as needed
    }
}
