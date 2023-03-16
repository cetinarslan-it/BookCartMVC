using Library.DataAccess.Repository.IRepository;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LibraryDBContext _db;
        internal DbSet<T> dbSet;

        public Repository(LibraryDBContext db)
        {
            _db = db;
            // _db.Products.Include(u=>u.Category).include(u=>u.CoverType);
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
           dbSet.Add(entity);
        }

        //Example =>   includeProperties = "Category,CoverType,,";     <= asingle string which contains commas twice
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperties != null)
            {
                foreach(var property in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
              
            }
            return query.ToList();
        }

        //Example =>   includeProperties = "Category,CoverType,,";     <= asingle string which contains commas twice
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }

            }

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
           dbSet.Remove(entity);
           _db.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
            _db.SaveChanges();
        }
    }
}
