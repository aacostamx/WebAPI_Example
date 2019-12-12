using mxm.biz.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mxm.biz.Repository
{
    public interface IStudentCourseRepository : IGenericRepository<StudentCourse>
    {
        IQueryable<StudentCourse> FindStudent(int studentId);
    }

}
