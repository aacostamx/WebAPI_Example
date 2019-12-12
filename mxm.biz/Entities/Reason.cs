using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Reason
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public bool? Active { get; set; }
    }
}
