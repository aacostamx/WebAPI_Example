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
    public class HierarchyRepository : GenericRepository<Hierarchy>, IHierarchyRepository
    {
        public HierarchyRepository(DbMxmContext context) :  base(context) { }

        public override Hierarchy Find(Expression<Func<Hierarchy, bool>> match)
        {
            return _context.Set<Hierarchy>()
                .Include(c => c.Permissions)
                .SingleOrDefault(match);
        }
    }
}
