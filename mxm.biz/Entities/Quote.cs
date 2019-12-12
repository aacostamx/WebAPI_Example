using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Quote
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public Schedule Schedule { get; set; }
        public Student Student { get; set; }
    }
}
