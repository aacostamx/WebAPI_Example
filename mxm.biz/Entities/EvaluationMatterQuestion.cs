using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class EvaluationMatterQuestion
    {
        public int Id { get; set; }
        public int EvaluationMatterId { get; set; }
        public string Question { get; set; }
        public bool? Active { get; set; }

        public EvaluationMatter EvaluationMatter { get; set; }
    }
}
