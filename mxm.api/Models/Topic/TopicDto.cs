using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class TopicDto
    {
        public int Id { get; set; }
        public int MatterId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool? Active { get; set; }

        public StudentTopicDto StudentTopic { get; set; }
    }
}
