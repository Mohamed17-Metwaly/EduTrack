using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Models.DTO
{
    public class CourseRegistrationDTO
    {
        public int StudentId { get; set; }
        public List<int> CourseIds { get; set; } = new();
    }
}
