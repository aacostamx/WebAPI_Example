using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class StudentMatterDto
    {
        public int Id { get; set; }
        public int StudentCourseId { get; set; }
        public int MatterId { get; set; }
        public double Progress { get; set; }

        //public MatterDto Matter { get; set; }
        //public StudentCourseDto StudentCourse { get; set; }
    }
}
