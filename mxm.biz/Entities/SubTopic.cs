using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class SubTopic
    {
        public SubTopic()
        {
            Contents = new HashSet<Content>();
            StudentSubTopics = new HashSet<StudentSubTopic>();
        }

        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

        public Topic Topic { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<StudentSubTopic> StudentSubTopics { get; set; }
    }
}
