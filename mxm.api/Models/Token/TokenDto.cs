using mxm.biz.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class TokenDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token1 { get; set; }
        public DateTime Date { get; set; }
        public bool? Active { get; set; }

    }
}
