using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class StudentContentDto
    {
        public int Id { get; set; }
        public int StudentCourseId { get; set; }
        public int ContentId { get; set; }
        public double Progress { get; set; }

        //public ContentDto Content { get; set; }
        //public StudentCourseDto StudentCourse { get; set; }
    }
}
