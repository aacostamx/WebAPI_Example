using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class StudentSubTopic
    {
        public int Id { get; set; }
        public int StudentCourseId { get; set; }
        public int SubTopicId { get; set; }
        public double Progress { get; set; }

        public StudentCourse StudentCourse { get; set; }
        public SubTopic SubTopic { get; set; }
    }
}
