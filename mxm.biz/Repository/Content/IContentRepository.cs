using mxm.biz.Entities;
using mxm.biz.Paged;
using System;
using System.Collections.Generic;
using System.Text;

namespace mxm.biz.Repository
{
    public interface IContentRepository : IGenericRepository<Content>
    {

        PagedList<Content> GetAllPagedFilter(int pageNumber, int pageSize, int subTopicId);
    }
}
