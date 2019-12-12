using Microsoft.EntityFrameworkCore;
using mxm.biz.Entities;
using mxm.biz.Repository;
using mxm.dal.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace mxm.dal.Repository
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(DbMxmContext context) : base(context) { }

        public override IQueryable<Course> FindBy(Expression<Func<Course, bool>> predicate)
        {
            var res = _context.Courses.Where(predicate).Include(co=> co.StudentCourses).Include(co=> co.EvaluationStudentCourses);
            return res;
        }
    }
}
