using mxm.biz.Entities;
using System;
using System.Collections.Generic;

namespace mxm.api.Models
{
    public class UserDto
    {
        public UserDto()
        {
            Hierarchies = new List<HierarchyDto>();
        }
        public int Id { get; set; }
        public int RolId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MotherName { get; set; }
        //public string Curp { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string Salt { get; set; }
        public string Avatar { get; set; }
        //public string Street { get; set; }
        //public string Building { get; set; }
        //public string Floor { get; set; }
        //public int? DistrictId { get; set; }
        //public string Mobile { get; set; }
        //public string Phone { get; set; }
        //public DateTime? DateOfBirth { get; set; }
        public bool Active { get; set; }
        public IList<HierarchyDto> Hierarchies { get; set; }
        public StudentDto Student { get; set; }

    }
}
