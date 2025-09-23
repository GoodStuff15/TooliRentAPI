using Application.Validators.BusinessValidation;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategory_Validation _validator;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ICategory_Validation validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IEnumerable<CategoryReadDTO>> GetAllCategories(CancellationToken ct = default)
        {
            var categories = await _unitOfWork.Categories.GetAsync(ct: ct);
            if (categories == null || !categories.Any())
            {
                return Enumerable.Empty<CategoryReadDTO>();
            }
            return categories.Select(c => new CategoryReadDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
        }

        public async Task<CategoryReadDTO?> GetCategoryById(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateCategory(CategoryCreateDTO dto, CancellationToken ct = default)
        {
            var validationResult = await _validator.CreateCategoryValidation(dto, ct);

            if (!validationResult)
            {
                return false; // Validation failed
            }


            var toCreate = _mapper.Map<Category>(dto);

            await _unitOfWork.Categories.AddAsync(toCreate, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return true; // Category created successfully

        }

        public async Task<bool> UpdateCategory(int id, CategoryUpdateDTO dto, CancellationToken ct = default)
        {
            var toUpdate = await _unitOfWork.Categories.GetByIdAsync(id, ct);

            if (toUpdate == null)
            {
                return false; // Category not found

            }

            await _unitOfWork.Categories.UpdateAsync(_mapper.Map(dto, toUpdate), ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return true; // Category updated successfully
        }

        public async Task<bool> DeleteCategory(int id, CancellationToken ct = default)
        {
            var toDelete = await _unitOfWork.Categories.GetByIdAsync(id, ct);
            if(toDelete == null)
            {
                return false; // Not found
            }

            await _unitOfWork.Categories.DeleteAsync(toDelete, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return true; // Deleted successfully
        }

        
    }
}
