using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Project
    {
        public Project()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public Company Company { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
