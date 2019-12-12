using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Schedule
    {
        public Schedule()
        {
            Quotes = new HashSet<Quote>();
        }

        public int Id { get; set; }
        public int LocationId { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Limit { get; set; }
        public bool? Active { get; set; }

        public Course Course { get; set; }
        public Location Location { get; set; }
        public ICollection<Quote> Quotes { get; set; }
    }
}
