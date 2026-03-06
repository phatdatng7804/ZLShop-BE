using Microsoft.AspNetCore.Mvc;
using ZLShop.Services.Interfaces;
using ZLShop.DTOs.Categories;

namespace ZLShop.Controllers.Categories;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _categoryService.GetAllAsync();
        return Ok(result);
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateCategoryDto request)
    {
        var result = await _categoryService.CreateAsync(request);
        return Ok(result);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateCategoryDto request)
    {
        var result = await _categoryService.UpdateAsync(id, request);
        return Ok(result);
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _categoryService.DeleteAsync(id);
        return Ok(result);
    }

    [HttpGet("deleted")]
    public async Task<IActionResult> GetDeletedAsync()
    {
        var result = await _categoryService.GetDeletedAsync();
        return Ok(result);
    }

    [HttpPut("restore/{id}")]
    public async Task<IActionResult> RestoreAsync(int id)
    {
        var result = await _categoryService.RestoreAsync(id);
        return Ok(result);
    }
}
