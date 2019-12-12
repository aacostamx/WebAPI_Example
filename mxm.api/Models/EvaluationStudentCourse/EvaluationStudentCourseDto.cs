using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class EvaluationStudentCourseDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        //public TimeSpan Time { get; set; }
        public int TotalQuestions { get; set; }
        public int Hits { get; set; }
        public int Qualification { get; set; }

        public CategoryDto Category { get; set; }
    }
}
