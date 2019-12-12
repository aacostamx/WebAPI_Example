using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Rol
    {
        public Rol()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
