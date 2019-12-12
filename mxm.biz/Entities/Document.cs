using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Document
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Resource { get; set; }
        public int? CourseId { get; set; }
        public int DocumentTypeId { get; set; }

        public DocumentType DocumentType { get; set; }
        public Student Student { get; set; }
    }
}
