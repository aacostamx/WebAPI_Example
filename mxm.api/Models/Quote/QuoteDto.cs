using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class QuoteDto
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public ScheduleDto Schedule { get; set; }
        //public StudentDto Student { get; set; }
    }
}
