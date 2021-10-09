using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OpenML.BookStore.Application.Interfaces
{
    public interface IUnitOfWork
    {
        void Add<T>(T obj) where T : class;

        T GetById<T>(int? id) where T : class;

        T AddObj<T>(T obj) where T : class;

        void Update<T>(T obj) where T : class;

        void Remove<T>(T obj) where T : class;

        IQueryable<T> Query<T>() where T : class;

        IQueryable<T> GetAll<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "") where T : class;

        T GetByOptions<T>(Expression<Func<T, bool>> predicate, string includeProperties = "")
            where T : class;

        void Commit();

        Task CommitAsync(CancellationToken cancellationToken);

        void Attach<T>(T obj) where T : class;
        IQueryable ExecuteQuery<T>(string spQuery, object[] parameters) where T : class;        
    }
}
