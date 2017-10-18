using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = Teg.Com.Data;
using Teg.Com.IBiz;
using System.Linq.Expressions;

namespace Teg.Com.Biz
{
    public class Repository<D, T> : IRepository<T> where T : class where D : DAL.Repository<T>, new()
    {
        protected D _dao = new D();
        #region -- Implement --

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Return the result</returns>
        public T GetById(int id)
        {
            var res = _dao.GetById(id);
            return res;
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Add(T entity)
        {
            _dao.Insert(entity);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Delete(T entity)
        {
            _dao.Delete(entity);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Update(T entity)
        {
            _dao.Update(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            var res = _dao.SearchFor(predicate);
            return res;
        }
        #endregion
    }
}