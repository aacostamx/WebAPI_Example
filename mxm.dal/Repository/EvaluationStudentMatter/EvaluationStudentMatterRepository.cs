using mxm.biz.Entities;
using mxm.biz.Repository;
using mxm.dal.DBContext;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;


namespace mxm.dal.Repository
{
    public class EvaluationStudentMatterRepository : GenericRepository<EvaluationStudentMatter>, IEvaluationStudentMatterRepository
    {
        public EvaluationStudentMatterRepository(DbMxmContext context): base(context) { }

        public override IQueryable<EvaluationStudentMatter> FindBy(Expression<Func<EvaluationStudentMatter, bool>> predicate)
        {
            var res = _context.EvaluationStudentMatters.Where(predicate).Include(esm => esm.Category);
            //return base.FindBy(predicate);
            return res;
        }
    }
}
