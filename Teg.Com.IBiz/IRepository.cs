using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Teg.Com.IBiz
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Add(T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Delete(T entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Update(T entity);

        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
    }
}
