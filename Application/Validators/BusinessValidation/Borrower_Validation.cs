using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.BusinessValidation
{
    
    public class Borrower_Validation : IBorrower_Validation
    {
        private readonly IUnitOfWork _unitOfWork;
        public Borrower_Validation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> DoesUserExistAsync(string userId, CancellationToken ct = default)
        {
            return await _unitOfWork.Users.DoesUserExist(userId, ct);
        }

    }
}
