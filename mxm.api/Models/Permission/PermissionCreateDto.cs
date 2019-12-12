using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class PermissionCreateDto
    {
        public int HierarchyId { get; set; }
        public int ScreenId { get; set; }
        public int PermissionTypeId { get; set; }
    }
}
