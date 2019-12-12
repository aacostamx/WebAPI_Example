using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public bool Active { get; set; }
    }
}
