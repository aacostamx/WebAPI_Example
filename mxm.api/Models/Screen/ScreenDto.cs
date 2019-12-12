using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class ScreenDto
    {
        //public ScreenDto()
        //{
        //    Permissions = new List<PermissionDto>();
        //}

        public int Id { get; set; }
        public int ScreenId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int Upward { get; set; }
        public string Icon { get; set; }
        public bool? Active { get; set; }

        //public IList<PermissionDto> Permissions { get; set; }
    }
}
