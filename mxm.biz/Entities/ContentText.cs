using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class ContentText
    {
        public int Id { get; set; }
        public int ContentDetailId { get; set; }
        public string Text { get; set; }

        public ContentDetail ContentDetail { get; set; }
    }
}
