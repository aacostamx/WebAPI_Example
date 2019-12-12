using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class UserAdminDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MotherName { get; set; }
        public string Curp { get; set; }
        public string Email { get; set; }
        public string Level { get; set; }
        public string CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string CourseName { get; set; }
        public bool? Active { get; set; }
        public IList<HierarchyDto> Hierarchies { get; set; }
    }

    public class UserStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MotherName { get; set; }
        public string Curp { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Active { get; set; }
    }
}
