using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class City
    {
        public City()
        {
            Districts = new HashSet<District>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }

        public State State { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
