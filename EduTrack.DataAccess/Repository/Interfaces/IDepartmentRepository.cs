using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.Models;

namespace EduTrack.DataAccess.Repository.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task UpdataAsync(Department department);
    }
}
