using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Teg.Com.IData
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Return the result</returns>
        T GetById(object id);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Insert(T entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Update(T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Delete(T entity);

        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
    }
}
