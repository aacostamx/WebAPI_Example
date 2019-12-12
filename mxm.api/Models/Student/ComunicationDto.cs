using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class ComunicationDto
    {
        public ComunicationDto()
        {
            Phones = new List<string>();
        }
        public string Whatsapp { get; set; }
        public IList<string> Phones { get; set; }
    }
}
