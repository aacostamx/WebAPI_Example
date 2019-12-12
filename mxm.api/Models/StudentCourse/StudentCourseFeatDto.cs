using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class StudentCourseFeatDto
    {
        public List<EvaluationStudentCourseDto> Courses { get; set; }
        public List<MatterDto> Matters { get; set; }
        public List<ConnectionTimeDto> ConnectionDurations { get; set; }
        public List<ConnectionDayDtos> ConnectionDays { get; set; }
    }
}
