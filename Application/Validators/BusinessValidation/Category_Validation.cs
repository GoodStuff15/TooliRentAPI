using Domain.DTOs;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.BusinessValidation
{
    public class Category_Validation : ICategory_Validation
    {
        private readonly IUnitOfWork _unitOfWork;

        public Category_Validation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateCategoryValidation(CategoryCreateDTO dto, CancellationToken ct)
        {
            // Check if category name already exists
            if (await DoesCategoryNameExist(dto.Name))
            {
                return false; // Category name already exists
            }
            // Additional validation rules can be added here

            return true; // Validation passed
        }

        public async Task<bool> DoesCategoryNameExist(string categoryName)
        {
            var categories = await _unitOfWork.Categories.GetAsync(filter: c => c.Name.ToLower() == categoryName.ToLower());

            if (categories.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
