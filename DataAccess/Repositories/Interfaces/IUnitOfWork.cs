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
        ToolRepository Tools { get; }

        CategoryRepository Categories { get; }

        ToolTypeRepository ToolTypes { get; }

        BookingRepository Bookings { get; }

        BorrowerRepository Borrowers { get; }

        UserRepository Users { get; }


        // Earlier version using generic repositories:

        //IGenericRepository<Tool> Tools { get; }
        //IGenericRepository<Category> Categories { get; }
        //IGenericRepository<ToolType> ToolTypes { get; }
        //IGenericRepository<Booking> Bookings { get; }
        //IGenericRepository<Borrower> Borrowers { get; }

        Task<bool> SaveChangesAsync(CancellationToken ct = default);
    }
}
