using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class PermissionType
    {
        public PermissionType()
        {
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
