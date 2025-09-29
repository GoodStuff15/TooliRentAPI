using Infrastructure;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.BusinessValidation
{
    internal class UserValidation : IUserValidation
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ToolContext _context;
        public UserValidation(IUnitOfWork unitOfWork, ToolContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;

        } 

        public async Task<bool> DoesUserExistAsync(string userId, CancellationToken ct = default)

        {
            var user = await _context.Users.FindAsync(userId, ct);

            return user != null;
        
        }
        public async Task<bool> IsEmailAlreadyRegistered(string email, CancellationToken ct = default)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == email, ct);

            return exists;
        }
    }
}
