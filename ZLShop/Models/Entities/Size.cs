namespace ZLShop.Models.Entities;

public class Size : BaseEntity
{
    public int Id {get; set;}
    public string Name {get; set;}
    public ICollection<ProductVariant> Variants { get; set; }
}