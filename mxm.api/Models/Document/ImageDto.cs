using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class ImageDto
    {
        
        public string image { get; set; }
        public IFormFile uri { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
