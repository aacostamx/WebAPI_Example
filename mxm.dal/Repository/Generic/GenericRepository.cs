﻿using Microsoft.EntityFrameworkCore;
using mxm.biz.Paged;
using mxm.biz.Repository;
using mxm.dal.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace mxm.dal.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbMxmContext _context;

        public GenericRepository(DbMxmContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsyn()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual T Add(T t)
        {
            _context.Set<T>().Add(t);
            _context.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsyn(T t)
        {
            _context.Set<T>().Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsyn(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _context.Set<T>().Find(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                _context.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsyn(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await _context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                await _context.SaveChangesAsync();
            }
            return exist;
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Exists(int id)
        {
            return _context.Set<T>().Find(id) != null ? true : false;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id) != null ? true : false;
        }

        public bool Exists(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match) != null ? true : false;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match) != null ? true : false;
        }

        public PagedList<T> GetAllPaged(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            int totalRows = _context.Set<T>().Count();
            List<T> result = _context.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(totalRows, result);
        }

        public async Task<PagedList<T>> GetAllPagedAsyn(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            int totalRows = await _context.Set<T>().CountAsync();
            List<T> result = await _context.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<T>(totalRows, result);
        }
    }
}
