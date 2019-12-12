using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Resource { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentTypeDto DocumentType { get; set; }
    }
}
