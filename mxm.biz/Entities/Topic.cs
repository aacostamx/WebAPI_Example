using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Topic
    {
        public Topic()
        {
            StudentTopics = new HashSet<StudentTopic>();
            SubTopics = new HashSet<SubTopic>();
        }

        public int Id { get; set; }
        public int MatterId { get; set; }
        public int Blocker { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool? Active { get; set; }

        public Matter Matter { get; set; }
        public ICollection<StudentTopic> StudentTopics { get; set; }
        public ICollection<SubTopic> SubTopics { get; set; }
    }
}
