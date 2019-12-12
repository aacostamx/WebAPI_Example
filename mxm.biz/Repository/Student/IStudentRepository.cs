using mxm.biz.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace mxm.biz.Repository
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Student Profile(int id);
    }
}
