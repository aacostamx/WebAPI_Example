using mxm.biz.Entities;
using mxm.biz.Repository;
using mxm.dal.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace mxm.dal.Repository
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(DbMxmContext context): base(context) { }
    }
}
