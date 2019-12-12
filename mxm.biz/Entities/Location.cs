using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Location
    {
        public Location()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public bool? Active { get; set; }

        public District District { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
