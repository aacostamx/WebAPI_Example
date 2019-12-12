using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class EvaluationMatter
    {
        public EvaluationMatter()
        {
            EvaluationMatterQuestions = new HashSet<EvaluationMatterQuestion>();
        }

        public int Id { get; set; }
        public int MatterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalQuestions { get; set; }
        public bool? Active { get; set; }

        public Matter Matter { get; set; }
        public ICollection<EvaluationMatterQuestion> EvaluationMatterQuestions { get; set; }
    }
}
