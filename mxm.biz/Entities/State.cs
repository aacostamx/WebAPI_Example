using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
