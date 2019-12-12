using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public ICollection<Document> Documents { get; set; }
    }
}
