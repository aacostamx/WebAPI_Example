using mxm.biz.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mxm.biz.Repository
{
    public interface IMatterRepository : IGenericRepository<Matter>
    {
        IQueryable<Matter> FindByStudentCourse(int studentCourseId);
    }
}
