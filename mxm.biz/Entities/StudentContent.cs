using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class StudentContent
    {
        public int Id { get; set; }
        public int StudentCourseId { get; set; }
        public int ContentId { get; set; }
        public double Progress { get; set; }

        public Content Content { get; set; }
        public StudentCourse StudentCourse { get; set; }
    }
}
