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
    public class EvaluationStudentCourseRepository : GenericRepository<EvaluationStudentCourse>, IEvaluationStudentCourseRepository
    {
        public EvaluationStudentCourseRepository(DbMxmContext context) : base(context) { }

        public override IQueryable<EvaluationStudentCourse> FindBy(Expression<Func<EvaluationStudentCourse, bool>> predicate)
        {
            var res = _context.EvaluationStudentCourses.Where(predicate).Include(esc => esc.Category);
            return res;
        }
    }
}
