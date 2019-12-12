using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Content
    {
        public Content()
        {
            ContentDetails = new HashSet<ContentDetail>();
            StudentContents = new HashSet<StudentContent>();
        }

        public int Id { get; set; }
        public int SubTopicId { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

        public SubTopic SubTopic { get; set; }
        public ICollection<ContentDetail> ContentDetails { get; set; }
        public ICollection<StudentContent> StudentContents { get; set; }
    }
}
