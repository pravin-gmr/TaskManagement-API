using API.Entities.Context;
using API.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace API.Repository.Impl
{
    /// <summary>
    /// Generic repository implementation for handling database operations.
    /// </summary>
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes an entity from the database.
        /// </summary>
        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a collection of entities from the database.
        /// </summary>
        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns the count of entities that match the given filter.
        /// </summary>
        public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.CountAsync(filter);
        }

        /// <summary>
        /// Retrieves a single entity that matches the given filter.
        /// </summary>
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query = tracked ? _dbSet : _dbSet.AsNoTracking();
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);
            }

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves all entities matching the given filter and includes related properties if specified.
        /// </summary>
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = filter != null ? _dbSet.Where(filter) : _dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);
            }
            return await query.ToListAsync();
        }

        /// <summary>
        /// Retrieves paginated data based on a given filter and ordering.
        /// </summary>
        public async Task<PaginationData<T>> GetAllByPaginationAsync(Expression<Func<T, bool>> filter, Expression<Func<T, dynamic>> orderBy,
            int pageNo, int pageSize, string? includeProperties = null)
        {
            ValidatePagination(ref pageNo, ref pageSize);

            IQueryable<T> query = filter != null ? _dbSet.Where(filter) : _dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);
            }

            return new PaginationData<T>(pageNo, pageSize)
            {
                TotalCount = await CountAsync(filter),
                Data = await query.OrderByDescending(orderBy).Skip(pageSize * (pageNo - 1)).Take(pageSize).ToListAsync()
            };
        }

        /// <summary>
        /// Ensures that page number and page size are valid.
        /// </summary>
        private static void ValidatePagination(ref int pageNumber, ref int pageSize)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 1;
        }
    }
}
