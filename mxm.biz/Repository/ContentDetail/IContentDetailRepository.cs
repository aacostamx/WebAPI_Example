using mxm.biz.Entities;
using mxm.biz.Paged;
using System;
using System.Collections.Generic;
using System.Text;

namespace mxm.biz.Repository
{
    public interface IContentDetailRepository : IGenericRepository<ContentDetail>
    {

        PagedList<ContentDetail> GetAllPagedFilter(int pageNumber, int pageSize, int contentId);
    }
}
