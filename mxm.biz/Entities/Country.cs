using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Country
    {
        public Country()
        {
            States = new HashSet<State>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<State> States { get; set; }
    }
}
