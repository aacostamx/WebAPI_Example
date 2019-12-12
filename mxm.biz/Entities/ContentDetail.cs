using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class ContentDetail
    {
        public ContentDetail()
        {
            ContentTexts = new HashSet<ContentText>();
        }

        public int Id { get; set; }
        public int ContentId { get; set; }
        public int ContentTypeId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool? Active { get; set; }

        public Content Content { get; set; }
        public ContentType ContentType { get; set; }
        public ICollection<ContentText> ContentTexts { get; set; }
    }
}
