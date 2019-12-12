using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Permission
    {
        public int Id { get; set; }
        public int HierarchyId { get; set; }
        public int ScreenId { get; set; }
        public int PermissionTypeId { get; set; }

        public Hierarchy Hierarchy { get; set; }
        public PermissionType PermissionType { get; set; }
        public Screen Screen { get; set; }
    }
}
