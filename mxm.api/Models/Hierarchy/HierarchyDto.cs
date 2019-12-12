using mxm.biz.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class HierarchyDto
    {

        public HierarchyDto()
        {
            Permissions = new List<PermissionDto>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int RolId { get; set; }
        public int Global { get; set; }
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int CourseId { get; set; }
        public bool? Active { get; set; }

        public List<PermissionDto> Permissions { get; set; }


    }
}
