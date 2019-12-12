using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class HierarchyCreateDto
    {
        public HierarchyCreateDto()
        {
            Permissions = new List<PermissionCreateDto>();
        }
        //[Required]
        //public int UserId { get; set; }
        [Required]
        public int RolId { get; set; }
        public int Global { get; set; }
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int CourseId { get; set; }

        public IList<PermissionCreateDto> Permissions { get; set; }
    }
}
