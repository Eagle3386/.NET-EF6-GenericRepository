using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace com.yourapp.data.services
{
    public class BaseService<TObject> where TObject : class
    {
        protected DataContext _context;

        /// <summary>
        /// The contructor requires an open DataContext to work with
        /// </summary>
        /// <param name="context">An open DataContext</param>
        public BaseService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        public TObject Get(int id)
        {
            return _context.Set<TObject>().Find(id);
        }

        /// <summary>
        /// Returns a single object with a primary key of the provided id
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        public async Task<TObject> GetAsync(int id)
        {
            return await _context.Set<TObject>().FindAsync(id);
        }

        /// <summary>
        /// Gets a queryable of all objects in the database
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <returns>An IQueryable of every object in the database</returns>
        public IQueryable<TObject> GetAll()
        {
            return _context.Set<TObject>();
        }

        /// <summary>
        /// Gets a queryable of all objects in the database
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <returns>An IQueryable of every object in the database</returns>
        public async Task<IQueryable<TObject>> GetAllAsync()
        {
            return await _context.Set<TObject>();
        }

        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="match">A Linq expression filter to find a single result</param>
        /// <returns>A single object which matches the expression filter. 
        /// If more than one object is found or if zero are found, null is returned</returns>
        public TObject Find(Expression<Func<TObject, bool>> match)
        {
            return _context.Set<TObject>().SingleOrDefault(match);
        }

        /// <summary>
        /// Returns a single object which matches the provided expression
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="match">A Linq expression filter to find a single result</param>
        /// <returns>A single object which matches the expression filter. 
        /// If more than one object is found or if zero are found, null is returned</returns>
        public async Task<TObject> FindAsync(Expression<Func<TObject, bool>> match)
        {
            return await _context.Set<TObject>().SingleOrDefaultAsync(match);
        }

        /// <summary>
        /// Returns a queryable which matches the provided expression
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="match">A linq expression filter to find one or more results</param>
        /// <returns>An IQueryable of object which match the expression filter</returns>
        public IQueryable<TObject> FindAll(Expression<Func<TObject, bool>> match)
        {
            return _context.Set<TObject>().Where(match);
        }

        /// <summary>
        /// Returns a queryable which matches the provided expression
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="match">A linq expression filter to find one or more results</param>
        /// <returns>An IQueryable of object which match the expression filter</returns>
        public async Task<IQueryable<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match)
        {
            return await _context.Set<TObject>().Where(match);
        }

        /// <summary>
        /// Inserts a single object to the database and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="t">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        public TObject Add(TObject t)
        {
            _context.Set<TObject>().Add(t);
            _context.SaveChanges();

            return t;
        }

        /// <summary>
        /// Inserts a single object to the database and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="t">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        public async Task<TObject> AddAsync(TObject t)
        {
            _context.Set<TObject>().Add(t);
            await _context.SaveChangesAsync();

            return t;
        }

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="tList">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        public IEnumerable<TObject> AddAll(IEnumerable<TObject> tList)
        {
            _context.Set<TObject>().AddRange(tList);
            _context.SaveChanges();

            return tList;
        }

        /// <summary>
        /// Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="tList">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        public async Task<IEnumerable<TObject>> AddAllAsync(IEnumerable<TObject> tList)
        {
            _context.Set<TObject>().AddRange(tList);
            await _context.SaveChangesAsync();

            return tList;
        }

        /// <summary>
        /// Updates a single object by finding it within the context and committing the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="updated">The updated object to apply to the database</param>
        /// <returns>The resulting updated object</returns>
        public TObject Update(TObject updated)
        {
            if (updated == null)
                return null;

            _context.Entry(updated).State = EntityState.Modified;
            _context.SaveChanges();

            return updated;
        }

        /// <summary>
        /// Updates a single object by finding it within the context and committing the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="updated">The updated object to apply to the database</param>
        /// <returns>The resulting updated object</returns>
        public async Task<TObject> UpdateAsync(TObject updated)
        {
            if (updated == null)
                return null;

            _context.Entry(updated).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return updated;
        }

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="t">The object to delete</param>
        public void Delete(TObject t)
        {
            _context.Set<TObject>().Remove(t);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes a single object from the database and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="t">The object to delete</param>
        public async Task<int> DeleteAsync(TObject t)
        {
            _context.Set<TObject>().Remove(t);

            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the count of the number of objects in the databse
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <returns>The count of the number of objects</returns>
        public int Count()
        {
            return _context.Set<TObject>().Count();
        }

        /// <summary>
        /// Gets the count of the number of objects in the databse
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <returns>The count of the number of objects</returns>
        public async Task<int> CountAsync()
        {
            return await _context.Set<TObject>().CountAsync();
        }
    }
}
