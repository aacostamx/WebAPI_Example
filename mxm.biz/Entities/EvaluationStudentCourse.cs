using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class EvaluationStudentCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int TotalQuestions { get; set; }
        public int Hits { get; set; }
        public double Qualification { get; set; }

        public Category Category { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
