using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class District
    {
        public District()
        {
            Locations = new HashSet<Location>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public int CityId { get; set; }

        public City City { get; set; }
        public ICollection<Location> Locations { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
