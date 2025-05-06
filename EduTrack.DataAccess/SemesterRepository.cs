using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.Models;

namespace EduTrack.DataAccess
{
    public class SemesterRepository : Repository<Semester>,ISemesterRepository
    {
        private readonly EduTrackContext _context;
        public SemesterRepository(EduTrackContext context) : base(context)
        {
            _context = context;
        }
        public async Task UpdateAsync(Semester semester)
        {
            var existingSemester = await _context.semesters.FindAsync(semester.Id);
            if (existingSemester != null)
            {
                _context.Entry(existingSemester).CurrentValues.SetValues(semester);
                await _context.SaveChangesAsync();
            }
        }
    }
}
