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
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter,
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                                    string includeProperties);
        Task<TEntity?> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(int id);
    }
}
