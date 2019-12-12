using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class LocationDto
    {
        public LocationDto()
        {
            Schedules = new List<ScheduleDto>();
        }

        public int Id { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public bool? Active { get; set; }

        public DistrictDto District { get; set; }
        public IList<ScheduleDto> Schedules { get; set; }
    }
}
