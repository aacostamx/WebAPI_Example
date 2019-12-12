using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RolId { get; set; }
    }
}
