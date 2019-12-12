using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class UserCompany
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Code { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Activated { get; set; }

        public Company Company { get; set; }
    }
}
