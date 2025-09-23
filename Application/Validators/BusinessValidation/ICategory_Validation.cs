using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.BusinessValidation
{
    public interface ICategory_Validation
    {
        Task<bool> DoesCategoryNameExist(string categoryName);

        Task<bool> CreateCategoryValidation(CategoryCreateDTO dto, CancellationToken ct);
    }
}
