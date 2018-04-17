using Data.DataContext;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AnnonsappenContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository()
        {
            _context = new AnnonsappenContext();
            _set = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _set.Add(entity);
            //_context.Update(entity);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _set.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            return GetAllIncluding(includeProperties).Where(predicate).ToList().FirstOrDefault()
                ?? throw new KeyNotFoundException($"Entity not found");
        }

        public IQueryable<TEntity> GetAllIncluding(params string[] includeProperties)
        {
            var queryable = _set as IQueryable<TEntity>;

            return includeProperties.Aggregate
                (queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _set.Update(entity);
            _context.SaveChanges();
        }
    }

 
}
