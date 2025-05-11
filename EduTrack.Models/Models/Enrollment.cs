using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTrack.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public decimal ?Grade { get; set; } 
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [ForeignKey("Semester")]
        public int SemesterId { get; set; }
        public Student Student { get; set; } 
        public Semester Semester { get; set; } 
        public Course Course { get; set; } 

    }
}
