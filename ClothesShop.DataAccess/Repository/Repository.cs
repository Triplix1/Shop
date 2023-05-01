using ClothesShop.DataAccess;
using ClothesShop.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            if (filter != null)
                return _context.Set<T>().Where(filter);

            return _context.Set<T>();
        }

        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {            
            return _context.Set<T>().FirstOrDefault(filter);            
        }

        public void Remove(T entity)
        {
            if (entity != null)
                _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
        }
    }
}
