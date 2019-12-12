using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class MatterDto
    {
        

        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        
        //public bool? Active { get; set; }

        public EvaluationMatterStudentMatterDto Evaluation { get; set; }
        //public StudentMatterDto StudentMatter { get; set; }
    }
}
