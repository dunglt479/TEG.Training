using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Teg.Com.IData;
using Teg.Com.Model;

namespace Teg.Com.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {

        #region -- Implement --

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Return the result</returns>
        public T GetById(object id)
        {
            var res = Entities.Find(id);
            return res;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                OpenConnection();

                Entities.Add(entity);
                DbContext.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (DbEntityValidationResult item in dbEx.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }

                var msg = dbEx.EntityValidationErrors
                    .SelectMany(validationErrors => validationErrors.ValidationErrors)
                    .Aggregate(string.Empty, (current, validationError) => current
                        + (string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage)
                        + Environment.NewLine));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                OpenConnection();

                Entities.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (DbEntityValidationResult item in dbEx.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }

                var msg = dbEx.EntityValidationErrors
                    .SelectMany(validationErrors => validationErrors.ValidationErrors)
                    .Aggregate(string.Empty, (current, validationError) => current
                        + (Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage)));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity">Entity object</param>
        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                //Audit(entity, "Delete");

                OpenConnection();

                Entities.Remove(entity);
                DbContext.Entry(entity).State = EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors
                    .SelectMany(validationErrors => validationErrors.ValidationErrors)
                    .Aggregate(string.Empty, (current, validationError) => current
                        + (Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage)));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                {
                    throw new ArgumentNullException("entity");
                }

                var res = Entities.Where(predicate);
                return res;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors
                    .SelectMany(validationErrors => validationErrors.ValidationErrors)
                    .Aggregate(string.Empty, (current, validationError) => current
                        + (string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage)
                        + Environment.NewLine));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        #endregion
        private IDbSet<T> _entities;

        /// <summary>
        /// DB context
        /// </summary>
        private BookingEntities _dbContext;
        protected BookingEntities DbContext
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items["BookingDbContext"] == null)
                    {
                        HttpContext.Current.Items["BookingDbContext"] = new BookingEntities();
                    }

                    return HttpContext.Current.Items["BookingDbContext"] as BookingEntities;
                }
                else
                {
                    // This will happen in console applications
                    if (_dbContext == null)
                    {
                        _dbContext = new BookingEntities();
                    }

                    return _dbContext;
                }
            }
            private set
            {
                _dbContext = value;
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                var res = _entities ?? (_entities = DbContext.Set<T>());
                return res;
            }
        }
        private void OpenConnection(DbContext context = null)
        {
            try
            {
                DbConnection connection;

                if (context == null)
                {
                    connection = DbContext.Database.Connection;
                }
                else
                {
                    connection = context.Database.Connection;
                }

                if (connection.State == ConnectionState.Closed
                    || connection.State == ConnectionState.Broken)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
