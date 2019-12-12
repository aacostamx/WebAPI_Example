using mxm.biz.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class CourseStudentDto
    {
        public CourseStudentDto()
        {
            EvaluationStudentCourses = new HashSet<EvaluationStudentCourseDto>();
            StudentCourses = new HashSet<StudentCourseDto>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool? Active { get; set; }

        public ICollection<EvaluationStudentCourseDto> EvaluationStudentCourses { get; set; }
        public ICollection<StudentCourseDto> StudentCourses { get; set; }
    }
}
