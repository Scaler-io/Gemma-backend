﻿using Gemma.Order.Application.Contracts.Persistance;
using Gemma.Order.Domain.Entities;
using Gemma.Order.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gemma.Order.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected OrderContext _orderContext;

        public RepositoryBase(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _orderContext.Set<T>().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _orderContext.Set<T>().Where(predicate).ToListAsync(); 
        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true)
        {
            IQueryable<T> query = _orderContext.Set<T>();
            if (disableTracking) query.AsNoTracking();
            if (!string.IsNullOrEmpty(includeString)) query = query.Include(includeString);
            if(predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).ToListAsync();
            return await query.ToListAsync();   
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true)
        {
            IQueryable<T> query = _orderContext.Set<T>();
            if (disableTracking) query.AsNoTracking();
            if(includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _orderContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
           _orderContext.Set<T>().Add(entity);
           await _orderContext.SaveChangesAsync(); 
           return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _orderContext.Entry(entity).State = EntityState.Modified;
            await _orderContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _orderContext.Set<T>().Remove(entity);
            await _orderContext.SaveChangesAsync(); 
        }
    }
}
