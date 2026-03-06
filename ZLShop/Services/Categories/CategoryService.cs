using ZLShop.DTOs.Categories;
using ZLShop.Models.Entities;
using ZLShop.Data;
using ZLShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ZLShop.Exceptions;
namespace ZLShop.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;
    public CategoryService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _context.Categories
            .ToListAsync();
        return categories.Select(c => new CategoryResponseDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
    }
    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto request)
    {
        var isExist = await _context.Categories
            .FirstOrDefaultAsync(c => c.Name == request.Name);
        if(isExist != null)
        {
            throw new BadRequestException("Tên danh mục đã tồn tại");
        }
        var category = new Category
        {
            Name = request.Name,
            CreatedAt = DateTime.UtcNow
        };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }
    public async Task<CategoryResponseDto> UpdateAsync(int id, UpdateCategoryDto request)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);
        if(category == null)
        {
            throw new BadRequestException("Danh mục không tồn tại");
        }
        category.Name = request.Name;
        await _context.SaveChangesAsync();
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if(category == null)
        {
            throw new BadRequestException("Danh mục không tồn tại");
        }
        category.IsDeleted = true;
        category.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<CategoryResponseDto>> GetDeletedAsync()
    {
        // Sử dụng IgnoreQueryFilters để "nhìn thấu" thùng rác, lấy ra các danh mục đã xóa
        var categories = await _context.Categories
            .IgnoreQueryFilters()
            .Where(c => c.IsDeleted)
            .ToListAsync();
            
        return categories.Select(c => new CategoryResponseDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
    }

    public async Task<bool> RestoreAsync(int id)
    {
        // Lấy danh mục từ thùng rác
        var category = await _context.Categories
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted);

        if (category == null)
        {
            throw new BadRequestException("Danh mục không tồn tại trong thùng rác");
        }

        // Khôi phục: đặt IsDeleted = false và xóa thời gian xóa
        category.IsDeleted = false;
        category.DeletedAt = null;

        await _context.SaveChangesAsync();
        return true;
    }
}