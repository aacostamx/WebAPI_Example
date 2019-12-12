using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Connection
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
