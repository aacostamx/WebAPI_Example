using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mxm.api.Models
{
    public class UserCreateDto
    {
        public UserCreateDto()
        {
            Hierarchies = new List<HierarchyDto>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MotherName { get; set; }
        //[Required]
        //public string Curp { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
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
        //public string sDateOfBirth { get; set; }
        public IList<HierarchyDto> Hierarchies { get; set; }

    }
}
