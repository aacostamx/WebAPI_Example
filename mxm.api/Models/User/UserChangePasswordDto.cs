using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class UserChangePasswordDto
    {
        //public int Id { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
