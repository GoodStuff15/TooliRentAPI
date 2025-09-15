using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.BusinessValidation
{
    public class Booking_Validation : IBooking_Validation
    {
        private readonly IUnitOfWork _unitOfWork;
        public Booking_Validation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<int>> AreToolsAvailable(IEnumerable<int> toolIds)
        {
            var unavailableToolIds = new List<int>();
            foreach (var toolId in toolIds)
            {
                var tool = await _unitOfWork.Tools.GetByIdAsync(toolId);
                if (!tool.IsAvailable)
                {
                    unavailableToolIds.Add(toolId);
                }
            }

            return unavailableToolIds;
        }

        public async Task<bool> IsBorrowerActive(int borrowerId)
        {
            var borrower = await _unitOfWork.Borrowers.GetByIdAsync(borrowerId);

            if(borrower == null || !borrower.IsActive)
            {
                return false;
            }

            return true;
        }
    }
}
