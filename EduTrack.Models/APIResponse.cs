using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrack.Models
{
    public class APIResponse
    {
        public string Status { get; set; }

        public bool IsSuccess { get; set; }

        public List<string> ErrorMessages { get; set; }

        public object Data { get; set; }

    }
}
