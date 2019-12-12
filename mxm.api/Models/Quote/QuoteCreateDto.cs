using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class QuoteCreateDto
    {
        public int Scheduled { get; set; }
        public int StudentId { get; set; }
    }
}
