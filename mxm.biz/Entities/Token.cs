using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Token
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token1 { get; set; }
        public DateTime Date { get; set; }
        public bool? Active { get; set; }

        public User User { get; set; }
    }
}
