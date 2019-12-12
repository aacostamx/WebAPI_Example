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
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(DbMxmContext context) : base(context) { }

        //public override Student Find(Expression<Func<Student, bool>> match)
        //{
        //    Student res = _context.Students.Include(st => st.User).SingleOrDefault(match);
        //    return res;
        //}

        public Student Profile(int id)
        {
            Student student = _context.Students.Where(s => s.Id == id)
                .Include(s => s.User)
                .Include(s=> s.Documents).ThenInclude(ds=> ds.DocumentType).SingleOrDefault();
            return student;
        }
    }
}
