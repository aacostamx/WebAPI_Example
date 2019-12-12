using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class ContentType
    {
        public ContentType()
        {
            ContentDetails = new HashSet<ContentDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public ICollection<ContentDetail> ContentDetails { get; set; }
    }
}
