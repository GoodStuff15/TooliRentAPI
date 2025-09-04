using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ToolContext _context;

        private IGenericRepository<Tool>? _toolRepository;
        private IGenericRepository<Category>? _categoryRepository;
        private IGenericRepository<ToolType>? _toolTypeRepository;
        private IGenericRepository<Booking>? _bookingRepository;
        private IGenericRepository<Borrower>? _borrowerRepository;


        public IGenericRepository<Tool> Tools => _toolRepository ??= new GenericRepository<Tool>(_context);
        public IGenericRepository<Category> Categories => _categoryRepository ??= new GenericRepository<Category>(_context);
        public IGenericRepository<ToolType> ToolTypes => _toolTypeRepository ??= new GenericRepository<ToolType>(_context);
        public IGenericRepository<Booking> Bookings => _bookingRepository ??= new GenericRepository<Booking>(_context);
        public IGenericRepository<Borrower> Borrowers => _borrowerRepository ??= new GenericRepository<Borrower>(_context);


        public UnitOfWork(ToolContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
