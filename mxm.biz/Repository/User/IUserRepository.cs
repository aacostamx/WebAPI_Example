using mxm.biz.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.biz.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        string HashPassword(string password);
        bool VerifyPassword(string hash, string password);
        string ExistUser(string email);
        IQueryable<User> FindByHierarchy(int CompanyId, int ProjectId, int CourseId);
    }
}
