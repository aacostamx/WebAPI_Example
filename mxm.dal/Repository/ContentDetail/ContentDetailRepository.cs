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
    public class ContentDetailRepository : GenericRepository<ContentDetail>, IContentDetailRepository
    {
        public ContentDetailRepository(DbMxmContext context) : base(context) { }

        public PagedList<ContentDetail> GetAllPagedFilter(int pageNumber, int pageSize, int contentId)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            var res = _context.ContentDetails.Where(cd => cd.ContentId == contentId)
                .Include(cd=> cd.ContentType)
                .Include(cd=> cd.ContentTexts);
            int totalRows = res.Count();
            List<ContentDetail> result = res.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<ContentDetail>(totalRows, result);
        }

        public override IQueryable<ContentDetail> FindBy(Expression<Func<ContentDetail, bool>> predicate)
        {
            var res = _context.ContentDetails.Where(predicate)
                .Include(cd => cd.ContentType)
                .Include(cd=> cd.ContentTexts);
            return res;
        }
    }
}
