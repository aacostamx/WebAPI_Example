using Microsoft.EntityFrameworkCore;
using mxm.biz.Entities;
using mxm.biz.Paged;
using mxm.biz.Repository;
using mxm.dal.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace mxm.dal.Repository
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        public ContentRepository(DbMxmContext context) : base(context) { }

        public PagedList<Content> GetAllPagedFilter(int pageNumber, int pageSize, int subTopicId)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            var res = _context.Contents.Where(cd => cd.SubTopicId == subTopicId);
            int totalRows = res.Count();
            List<Content> result = res.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<Content>(totalRows, result);
        }

    }
}
