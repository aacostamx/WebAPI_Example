using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Matter
    {
        public Matter()
        {
            EvaluationMatters = new HashSet<EvaluationMatter>();
            EvaluationStudentMatters = new HashSet<EvaluationStudentMatter>();
            StudentMatters = new HashSet<StudentMatter>();
            Topics = new HashSet<Topic>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool? Active { get; set; }

        public Course Course { get; set; }
        public ICollection<EvaluationMatter> EvaluationMatters { get; set; }
        public ICollection<EvaluationStudentMatter> EvaluationStudentMatters { get; set; }
        public ICollection<StudentMatter> StudentMatters { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}
