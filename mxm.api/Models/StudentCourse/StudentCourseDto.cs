using mxm.biz.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class StudentCourseDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Progress { get; set; }

        public CourseDto Course { get; set; }
        public EvaluationStudentCourseDto EvaluationStudentCourse { get; set; }
    }
}
