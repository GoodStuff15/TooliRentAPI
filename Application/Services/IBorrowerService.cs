using Domain.DTOs;
using Domain.DTOs.ResponseDTOs;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IBorrowerService
    {
        Task<IEnumerable<BorrowerReadDTO>> GetAllAsync(CancellationToken ct = default);
        Task<BorrowerReadDTO?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<IEnumerable<BorrowerReadDTO>> GetAllFilteredAsync(BorrowerFilterDTO dto, CancellationToken ct = default);

        Task<BorrowerCreate_ResponseDTO> CreateAsync(BorrowerCreateDTO dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, BorrowerUpdateDTO dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);

        public Task<BorrowerReadDTO?> GetByUserIdAsync(string userId, CancellationToken ct);

        public Task AddRefreshToken(RefreshToken rt, CancellationToken ct);

        public Task<bool> UpdateStatus(int id, CancellationToken ct = default);
    }
}
