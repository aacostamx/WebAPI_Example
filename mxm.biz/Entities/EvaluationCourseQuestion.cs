using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class EvaluationCourseQuestion
    {
        public int Id { get; set; }
        public int EvaluationCourseId { get; set; }
        public string Question { get; set; }
        public bool? Active { get; set; }

        public EvaluationCourse EvaluationCourse { get; set; }
    }
}
