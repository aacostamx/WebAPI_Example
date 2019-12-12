using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class ScheduleDto
    {
        //public ScheduleDto()
        //{
        //    Quotes = new List<QuoteDto>();
        //}

        public int Id { get; set; }
        public int LocationId { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        //public TimeSpan Time { get; set; }
        public int Limit { get; set; }
        public int Available { get; set; }
        public bool? Active { get; set; }

        //public LocationDto Location { get; set; }
        //public IList<QuoteDto> Quotes { get; set; }
    }
}
