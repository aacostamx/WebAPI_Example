using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class StudentTopic
    {
        public int Id { get; set; }
        public int StudentCourseId { get; set; }
        public int TopicId { get; set; }
        public double Progress { get; set; }

        public StudentCourse StudentCourse { get; set; }
        public Topic Topic { get; set; }
    }
}
