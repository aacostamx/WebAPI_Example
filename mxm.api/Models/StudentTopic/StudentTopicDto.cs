using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class StudentTopicDto
    {
        public int Id { get; set; }
        public int StudentCourseId { get; set; }
        public int TopicId { get; set; }
        public double Progress { get; set; }

        //public StudentCourseDto StudentCourse { get; set; }
        //public TopicDto Topic { get; set; }
    }
}
