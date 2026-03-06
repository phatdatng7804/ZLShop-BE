namespace ZLShop.Models.Entities;

public class Color : BaseEntity
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string HexCode {get; set;}
    public ICollection<ProductVariant> Variants { get; set; }
}