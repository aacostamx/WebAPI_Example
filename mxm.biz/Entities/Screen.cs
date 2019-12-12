using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Screen
    {
        public Screen()
        {
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int Upward { get; set; }
        public string Icon { get; set; }
        public bool? Active { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
