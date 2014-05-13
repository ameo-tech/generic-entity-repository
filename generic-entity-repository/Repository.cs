using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Entity.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataBaseSettings _context;
        private IDbSet<T> _entities;
        public Repository(DataBaseSettings context)
        {
            _context = context;
        }

        /// <summary>
        /// Add this entity in to database.
        /// </summary>
        /// <param name="entity">entity is class</param>
        public void Insert(T entity)
        {
            if (entity != null)
            {
                this.Entities.Add(entity);
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// get T Type data on the basis of expression.  
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>its return T type entity</returns>
        public T Get(Func<T, bool> expression)
        {
            return this.Entities.FirstOrDefault(expression);
        }

        /// <summary>
        /// Fetch all records .
        /// </summary>
        /// <returns></returns>
        /// 
        public IList<T> FetchAll()
        {
            return GetAll().ToList();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// get particular record from database on the basis of id.
        /// </summary>
        /// <param name="id">Id is interger value</param>
        /// <returns></returns>
        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        /// <summary>
        /// Check data is exist in database or not.  
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>its return true or false</returns>
        public bool IsExist(Func<T, bool> expression)
        {
            return this.Entities.Any(expression);
        }

        /// <summary>
        /// Update this entity in to database.
        /// </summary>
        /// <param name="entity">entity is class</param>
        public void Update(T entity)
        {
            if (entity != null)
            {
                _context.Set<T>().Attach(entity);
                _context.Entry<T>(entity).State = EntityState.Modified;
                
                _context.SaveChanges();
            }
        }

        public IList<T> FetchAll(Expression<Func<T, bool>> exp)
        {
            return GetAll(exp).ToList();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp);
        }


        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public void Delete(T entity)
        {
            
            if (entity != null)
            {
                _context.Set<T>().Attach(entity);
                _context.Entry<T>(entity).State = EntityState.Deleted;

                _context.SaveChanges();
            }
        }
    }
}
