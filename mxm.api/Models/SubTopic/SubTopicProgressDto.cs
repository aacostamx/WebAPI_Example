using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class SubTopicProgressDto
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public double Progress { get; set; }

        //public StudentSubTopicDto StudentSubTopic { get; set; }
    }
}
