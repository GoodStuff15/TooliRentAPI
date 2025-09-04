using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Tool> Tools { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<ToolType> ToolTypes { get; }
        IGenericRepository<Booking> Bookings { get; }
        IGenericRepository<Borrower> Borrowers { get; }

        Task<int> SaveChanges(CancellationToken ct = default);
    }
}
