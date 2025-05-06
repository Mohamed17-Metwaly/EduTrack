using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.DataAccess
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        private readonly EduTrackContext _context;
        public EnrollmentRepository(EduTrackContext db) : base(db)
        {
            _context = db;
        }
        public async Task UpdateAsync(Enrollment enrollment)
        {
            var existingEnrollment = await _context.Departments.FindAsync(enrollment.Id);
            if (existingEnrollment != null)
            {
                _context.Entry(existingEnrollment).CurrentValues.SetValues(enrollment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
