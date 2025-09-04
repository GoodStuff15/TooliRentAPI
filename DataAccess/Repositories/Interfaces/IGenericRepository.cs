using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties, CancellationToken ct = default, Expression<Func<TEntity, bool>> filter = null,
                                                    Func<IQueryable<TEntity>, 
                                                    IOrderedQueryable<TEntity>> orderBy = null);
        Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        Task Update(TEntity entity, CancellationToken ct = default);
        Task Delete(int id, CancellationToken ct = default);
    }
}
