using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.DataAccess.Repository.Interfaces;
using EduTrack.Models;

namespace EduTrack.DataAccess
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly EduTrackContext _context;
        public DepartmentRepository(EduTrackContext context) : base(context)
        {
            _context = context;
        }
        public async Task UpdataAsync(Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department.Id);
            if (existingDepartment != null)
            {
                _context.Entry(existingDepartment).CurrentValues.SetValues(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
