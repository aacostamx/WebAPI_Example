using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }

        public Student Student { get; set; }
    }
}
