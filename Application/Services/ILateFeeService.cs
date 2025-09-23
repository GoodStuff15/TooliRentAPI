using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ILateFeeService
    {
        Task<IEnumerable<LateFeeReadDTO>> GetAllLateFees(CancellationToken ct = default);
        Task<IEnumerable<LateFeeReadDTO>> GetLateFeesByUserId(int id, CancellationToken ct = default);
        Task<bool> DeleteLateFee(int id, CancellationToken ct = default);
        Task<bool> MarkLateFeeAsPaid(int id, CancellationToken ct = default);
    }
}
