using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class HierarchyUpdateDto
    {
        public int UserId { get; set; }
        public int RolId { get; set; }
        public int Global { get; set; }
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int CourseId { get; set; }
        public bool Active { get; set; }
    }
}
