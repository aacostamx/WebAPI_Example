using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class StateDto
    {
        public StateDto()
        {
            Cities = new List<CityDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        //public Country Country { get; set; }
        public IList<CityDto> Cities { get; set; }
    }
}
