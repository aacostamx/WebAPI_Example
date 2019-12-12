using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class StudentSubTopicDto
    {
        public int Id { get; set; }
        public int StudentCourseId { get; set; }
        public int SubTopicId { get; set; }
        public double Progress { get; set; }
    }
}
