using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ToolContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ToolContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
        {
           
            if(entity != null)
              {
                 _dbSet.Remove(entity);
            }

            await Task.CompletedTask;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(string includeProperties = "",CancellationToken ct = default, Expression<Func<TEntity, bool>> filter = null,
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            foreach(var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
               return await query.ToListAsync();
            }
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
        {
           
            return await _dbSet.FindAsync(id, ct);


        }

        public async Task UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            
            await Task.CompletedTask;
        }
    }
}
