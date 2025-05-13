using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using ERP.Data.Data;
using Microsoft.Extensions.Logging;
using ERP.Core.Interfaces.Repositories;

namespace ERP.Data.Repository
{
    public class GenericRepository<T>(
        AppDbContext context,
        ILogger logger) : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        protected readonly DbSet<T> _dbSet = context.Set<T>();
        protected readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} for entity {EntityType}", nameof(GetAllAsync), typeof(T).Name);
                return [];
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet.AsNoTracking();
                query = ApplyIncludes(query, includes);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} with includes for entity {EntityType}", nameof(GetAllAsync), typeof(T).Name);
                return [];
            }
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} for entity {EntityType} with ID {Id}", nameof(GetByIdAsync), typeof(T).Name, id);
                return null;
            }
        }

        public virtual async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                // Get the primary key property name
                var keyName = (_context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties
                    .Select(x => x.Name).FirstOrDefault()) ?? throw new InvalidOperationException($"Primary key not found for entity {typeof(T).Name}");

                // Create the query with includes
                IQueryable<T> query = _dbSet.AsNoTracking();
                query = ApplyIncludes(query, includes);

                // Find by ID using the key property
                return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} with includes for entity {EntityType} with ID {Id}",
                    nameof(GetByIdAsync), typeof(T).Name, id);
                return null;
            }
        }

        public virtual async Task<int> CountAsync()
        {
            try
            {
                return await _dbSet.AsNoTracking().CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} for entity {EntityType}", nameof(CountAsync), typeof(T).Name);
                throw new Exception(ex.Message); // Re-throw to let caller handle the error appropriately
            }
        }

        public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> criteria)
        {
            try
            {
                return await _dbSet.AsNoTracking().SingleOrDefaultAsync(criteria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} for entity {EntityType}", nameof(FindAsync), typeof(T).Name);
                return null;
            }
        }

        public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet.AsNoTracking();
                query = ApplyIncludes(query, includes);
                return await query.SingleOrDefaultAsync(criteria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} with includes for entity {EntityType}",
                    nameof(FindAsync), typeof(T).Name);
                return null;
            }
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria)
        {
            try
            {
                return await _dbSet.AsNoTracking().Where(criteria).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} for entity {EntityType}", nameof(FindAllAsync), typeof(T).Name);
                return [];
            }
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet.AsNoTracking();
                query = ApplyIncludes(query, includes);
                return await query.Where(criteria).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} with includes for entity {EntityType}",
                    nameof(FindAllAsync), typeof(T).Name);
                return [];
            }
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            try
            {
                await _dbSet.AddAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} for entity {EntityType}", nameof(AddAsync), typeof(T).Name);
                throw new Exception(ex.Message); 
            }
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            try
            {
                var existingEntity = await _dbSet.FindAsync(entity) ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} with ID {entity} not found.");
                _dbSet.Update(entity);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} for entity {EntityType}", nameof(UpdateAsync), typeof(T).Name);
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);
            try
            {
                #region this code lead to more database queries
                // var existingEntities = await _dbSet.Where(e => entities.Contains(e)).ToListAsync();
                // if (existingEntities.Count != entities.Count())
                //     throw new InvalidOperationException("Some entities do not exist in the database.");

                // Update the entities in the DbSet without triggering change trackering
                #endregion
                _dbSet.UpdateRange(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} for entity {EntityType}", nameof(UpdateRangeAsync), typeof(T).Name);
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    return false;

                _dbSet.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} for entity {EntityType} with ID {Id}",
                    nameof(DeleteAsync), typeof(T).Name, id);
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            try
            {
             
                _dbSet.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method} for entity {EntityType}",
                    nameof(DeleteRangeAsync), typeof(T).Name);
                throw new Exception(ex.Message);
            }
        }

        // Helper method to apply includes
        private static IQueryable<T> ApplyIncludes(IQueryable<T> query, Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
    }
}