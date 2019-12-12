using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class StudentCreateDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Curp { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public int DistrictId { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public DateTime DateofBirth { get; set; }
    }
}
