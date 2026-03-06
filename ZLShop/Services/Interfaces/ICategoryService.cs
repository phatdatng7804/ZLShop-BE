using ZLShop.DTOs.Categories;

namespace ZLShop.Services.Interfaces;

public interface ICategoryService
{   
    Task<List<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto> CreateAsync(CreateCategoryDto request);
    Task<CategoryResponseDto> UpdateAsync(int id, UpdateCategoryDto request);
    Task<bool> DeleteAsync(int id);
    Task<List<CategoryResponseDto>> GetDeletedAsync();
    Task<bool> RestoreAsync(int id);
}