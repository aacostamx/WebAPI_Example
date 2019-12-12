using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Company
    {
        public Company()
        {
            Projects = new HashSet<Project>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
