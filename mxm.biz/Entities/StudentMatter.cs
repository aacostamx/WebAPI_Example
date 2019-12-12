using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class StudentMatter
    {
        public int Id { get; set; }
        public int StudentCourseId { get; set; }
        public int MatterId { get; set; }
        public double Progress { get; set; }

        public Matter Matter { get; set; }
        public StudentCourse StudentCourse { get; set; }
    }
}
