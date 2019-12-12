using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Tutorial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool? Active { get; set; }
    }
}
