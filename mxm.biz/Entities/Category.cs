using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Category
    {
        public Category()
        {
            EvaluationStudentCourses = new HashSet<EvaluationStudentCourse>();
            EvaluationStudentMatters = new HashSet<EvaluationStudentMatter>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int MarginTop { get; set; }
        public int MarginBottom { get; set; }
        public bool? Active { get; set; }

        public ICollection<EvaluationStudentCourse> EvaluationStudentCourses { get; set; }
        public ICollection<EvaluationStudentMatter> EvaluationStudentMatters { get; set; }
    }
}
