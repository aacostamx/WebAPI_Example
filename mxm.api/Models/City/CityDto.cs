using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class CityDto
    {
        public CityDto()
        {
            Districts = new List<DistrictDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }

        public StateDto State { get; set; }
        public IList<DistrictDto> Districts { get; set; }
    }
}
