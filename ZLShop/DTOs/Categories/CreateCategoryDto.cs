using System.ComponentModel.DataAnnotations;

namespace ZLShop.DTOs.Categories;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
    [MinLength(3, ErrorMessage = "Tên danh mục phải có ít nhất 3 ký tự")]
    public string Name { get; set; }
}
