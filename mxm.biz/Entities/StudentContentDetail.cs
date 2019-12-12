using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class StudentContentDetail
    {
        public int Id { get; set; }
        public int StudentCourseId { get; set; }
        public int ContentDetailId { get; set; }
        public bool? Viewed { get; set; }
    }
}
