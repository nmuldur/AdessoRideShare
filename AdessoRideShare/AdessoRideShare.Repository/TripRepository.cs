using AdessoRideShare.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdessoRideShare.Repository
{
    public class TripRepository<T> : ITripRepository<T> where T : class
    {
        private AdessoRideShareDbContext context;
        private DbSet<T> dbSet;

        public TripRepository(AdessoRideShareDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public virtual async Task<bool> AddAsync(T entity)
        {
            try
            {
                context.Set<T>().Add(entity);
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                return await Task.FromResult(false);
            }
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var result = context.Set<T>().Where(i => true);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.ToListAsync();
        }


        public virtual async Task<List<T>> SearchByAsync(Expression<Func<T, bool>> searchBy, params Expression<Func<T, object>>[] includes)
        {
            var result = context.Set<T>().Where(searchBy);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.ToListAsync();
        }
        public virtual async Task<T> FindByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = context.Set<T>().Where(predicate);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.FirstOrDefaultAsync();
        }

        public virtual async Task<bool> Any(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = await context.Set<T>().AnyAsync(predicate);
            return result;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                context.Set<T>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;

                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                return await Task.FromResult(false);
            }
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await context.Set<T>().FindAsync(id);
        }
    }
}
