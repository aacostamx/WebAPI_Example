using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class EvaluationCourse
    {
        public EvaluationCourse()
        {
            EvaluationCourseQuestions = new HashSet<EvaluationCourseQuestion>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalQuestions { get; set; }
        public bool? Active { get; set; }

        public Course Course { get; set; }
        public ICollection<EvaluationCourseQuestion> EvaluationCourseQuestions { get; set; }
    }
}
