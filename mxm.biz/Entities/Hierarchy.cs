using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Hierarchy
    {
        public Hierarchy()
        {
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int RolId { get; set; }
        public int? Global { get; set; }
        public int? CompanyId { get; set; }
        public int? ProjectId { get; set; }
        public int? CourseId { get; set; }
        public bool? Active { get; set; }

        public User User { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
