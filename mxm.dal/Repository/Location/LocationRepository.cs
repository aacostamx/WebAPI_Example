using mxm.biz.Entities;
using mxm.biz.Repository;
using mxm.dal.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace mxm.dal.Repository
{
    public class LocationRepository :GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(DbMxmContext context): base(context) { }
    }
}
