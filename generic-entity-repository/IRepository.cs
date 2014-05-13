using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Entity.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Add this entity in to database.
        /// </summary>
        /// <param name="entity">entity is class</param>
        void Insert(T entity);

        /// <summary>
        /// Update this entity in to database.
        /// </summary>
        /// <param name="entity">entity is class</param>
        void Update(T entity);

        /// <summary>
        /// Fetch all records .
        /// </summary>
        /// <returns></returns>
        IList<T> FetchAll();

        /// <summary>
        /// get particular record from database on the basis of id.
        /// </summary>
        /// <param name="id">Id is interger value</param>
        /// <returns></returns>
        T GetById(object id);
                

        /// <summary>
        /// Check data is exist in database or not.  
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>its return true or false</returns>
        bool IsExist(Func<T, bool> expression);


        /// <summary>
        /// get T Type data on the basis of expression.  
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>its return T type entity</returns>
        T Get(Func<T, bool> expression);

        /// <summary>
        /// Fetch all on condition basis.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IList<T> FetchAll(Expression<Func<T, bool>> exp);

        /// <summary>
        /// Fetch on condition specified as func but return IQueryable collection.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> exp);

        /// <summary>
        /// Return an instance of table itself.
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
    }
}
