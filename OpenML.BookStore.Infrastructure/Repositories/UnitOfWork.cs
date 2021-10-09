using Microsoft.EntityFrameworkCore;
using OpenML.BookStore.Application.Interfaces;
using OpenML.BookStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenML.BookStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private BookStoreContext _dbContext;

        public UnitOfWork(BookStoreContext context)
        {
            _dbContext = context;
        }

        public void Add<T>(T obj)
            where T : class
        {
            var set = _dbContext.Set<T>();
            set.Add(obj);
        }

        public T GetById<T>(int? id)
            where T : class
        {
            return _dbContext.Set<T>().Find(id);
        }

        public T AddObj<T>(T obj)
            where T : class
        {
            // obj = this.SetColumns(obj, "add", _currentUserService.UserId);
            var set = _dbContext.Set<T>();
            set.Add(obj);
            //_dbContext.Entry(obj).State = EntityState.Added;
            return obj;
        }

        public void Update<T>(T obj)
            where T : class
        {
            // obj = this.SetColumns(obj, "update", _currentUserService.UserId);
            var set = _dbContext.Set<T>();
            set.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

        void IUnitOfWork.Remove<T>(T obj)
        {
            var set = _dbContext.Set<T>();
            set.Remove(obj);
        }

        public IQueryable<T> Query<T>()
            where T : class
        {
            return _dbContext.Set<T>();
        }

        public virtual IQueryable<T> GetAll<T>(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
            where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();
            }

            return query.AsQueryable<T>();
        }

        public virtual T GetByOptions<T>(Expression<Func<T, bool>> predicate, string includeProperties = "")
            where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Attach<T>(T newUser) where T : class
        {
            var set = _dbContext.Set<T>();
            set.Attach(newUser);
        }

        public void Dispose()
        {
            _dbContext = null;
        }

        public IQueryable ExecuteQuery<T>(string spQuery, object[] parameters) where T : class
        {
            return _dbContext.Set<T>().FromSqlRaw(spQuery, parameters);
        }       
    }
}
