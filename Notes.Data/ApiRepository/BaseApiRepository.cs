using Microsoft.EntityFrameworkCore;
using Notes.Data.ApiRepository.IApiRepository;
using Notes.Data.AppDbContext;
using Notes.Data.Repository.IRepository;
using NuGet.ContentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.ApiRepository
{
    public class BaseApiRepository<T> : IBaseApiRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> dbSet;

        public BaseApiRepository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }
        public void Add(T Entity)
        {
            dbSet.Add(Entity);
        }

        public T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        public void Remove(T Entity)
        {
            dbSet.Remove(Entity);
        }
    }
}
