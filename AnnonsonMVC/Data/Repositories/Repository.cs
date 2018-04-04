﻿using Data.DataContext;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly annonsappenContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository()
        {
            _context = new annonsappenContext();
            _set = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _set.Add(entity);
            _context.Update(entity);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _set.ToList();
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
    }

 
}