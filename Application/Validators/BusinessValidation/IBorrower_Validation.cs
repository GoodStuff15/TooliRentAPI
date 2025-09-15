using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.BusinessValidation
{
    public interface IBorrower_Validation
    {
        Task<bool> DoesUserExistAsync(string userId, CancellationToken ct = default);
    }
}
