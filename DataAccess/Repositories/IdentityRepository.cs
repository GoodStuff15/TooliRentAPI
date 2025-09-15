using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly ToolContext _context;
        public IdentityRepository(ToolContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesUserExist(string userId, CancellationToken ct = default)
        {
            var user = await _context.Users.FindAsync(userId, ct);

            return user != null;
        }

        public async Task AddRefreshTokenAsync(RefreshToken rt, CancellationToken ct = default)
        {
            await _context.RefreshTokens.AddAsync(rt, ct);
            await _context.SaveChangesAsync(ct);
        }
    }
}
