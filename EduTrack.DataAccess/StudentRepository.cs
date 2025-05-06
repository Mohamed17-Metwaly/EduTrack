using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.Models;

namespace EduTrack.DataAccess
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly EduTrackContext _context;
        public StudentRepository(EduTrackContext context) : base(context)
        {
            _context = context;
        }
        public async Task UpdateAsync(Student student)
        {
            var existingStudent = await _context.Students.FindAsync(student.Id);
            if (existingStudent != null)
            {
                _context.Entry(existingStudent).CurrentValues.SetValues(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}
