using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IIdentityRepository
    {

        public Task<bool> DoesUserExist(string userId, CancellationToken ct = default);

        public Task AddRefreshTokenAsync(RefreshToken rt, CancellationToken ct = default);
    }
}
