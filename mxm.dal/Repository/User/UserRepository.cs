using mxm.biz.Entities;
using mxm.biz.Repository;
using mxm.dal.DBContext;
using CryptoHelper;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;


namespace mxm.dal.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbMxmContext context) : base(context) { }

        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        //public string CreateSalt(int size)
        //{
        //    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //    var buff = new byte[size];
        //    rng.GetBytes(buff);
        //    return Convert.ToBase64String(buff);
        //}


        public string ExistUser(string email)
        {
            var pass = _context.Users.Find(email);
            return pass.Password;
        }

        public bool VerifyPassword(string hash, string password)
        {
            return Crypto.VerifyHashedPassword(hash, password);
        }

        public override User Add(User user)
        {
            user.Password = HashPassword(user.Password);
            return base.Add(user);
        }

        public override User Update(User user, object id)
        {
            user.Modified = DateTime.Now;
            return base.Update(user, id);
        }

        //public IQueryable<User> FindBy(Expression<Func<User, bool>> predicate, Hierarchy hierarchy)
        //{
        //    return base.FindBy(predicate);
        //}

        public override User Find(Expression<Func<User, bool>> match)
        {
            var res = _context.Users
                .Include(c => c.Hierarchies) //lista de jerarquias por usuario
                    .ThenInclude(to => to.Permissions) //lista de permisos por jerarquia
                        .ThenInclude(pe => pe.Screen) //elemento screen por permiso
                .Include(c=> c.Student)
                .SingleOrDefault(match);

            return res;
        }

        public IQueryable<User> FindByHierarchy(int CompanyId, int ProjectId, int CourseId)
        {
            IQueryable<User> res;
            
            if (CompanyId >0)
            {
                var subres = _context.Users.Join(_context.Hierarchies, usr => usr.Id, hie => hie.UserId, (usr, hie) => new { User = usr, Hierarchy = hie }).Where(us => us.Hierarchy.CompanyId == CompanyId);
                if (ProjectId  > 0)
                {
                    subres = subres.Where(sb => sb.Hierarchy.ProjectId == ProjectId);
                    if (CourseId > 0)
                    {
                        subres = subres.Where(sb => sb.Hierarchy.CourseId == CourseId);
                    }
                }
                res = subres.Select(sb => sb.User).Include(us=> us.Hierarchies);

            }
            else
                res = _context.Users.Include(us=> us.Hierarchies).Where(us => us.RolId == 2);
            
            return res;
        }

        
    }
}
