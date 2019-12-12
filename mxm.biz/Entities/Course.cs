using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Course
    {
        public Course()
        {
            Connections = new HashSet<Connection>();
            EvaluationCourses = new HashSet<EvaluationCourse>();
            EvaluationStudentCourses = new HashSet<EvaluationStudentCourse>();
            Matters = new HashSet<Matter>();
            Schedules = new HashSet<Schedule>();
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool? Active { get; set; }

        public Project Project { get; set; }
        public ICollection<Connection> Connections { get; set; }
        public ICollection<EvaluationCourse> EvaluationCourses { get; set; }
        public ICollection<EvaluationStudentCourse> EvaluationStudentCourses { get; set; }
        public ICollection<Matter> Matters { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
