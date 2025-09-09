using Domain.DTOs;
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

        Task<BorrowerReadDTO?> GetAllFilteredAsync(BorrowerFilterDTO dto, CancellationToken ct = default);

        Task<int> CreateAsync(BorrowerCreateDTO dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, BorrowerUpdateDTO dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
