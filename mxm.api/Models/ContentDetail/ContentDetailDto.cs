using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class ContentDetailDto
    {
        public ContentDetailDto()
        {
            ContentTexts = new List<ContentTextDto>();
        }

        public int Id { get; set; }
        public int ContentId { get; set; }
        public int ContentTypeId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool? Active { get; set; }

        public IList<ContentTextDto> ContentTexts { get; set; }
        public ContentTypeDto ContentType { get; set; }

    }
}
