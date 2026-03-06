namespace ZLShop.Models.Entities;

public class ProductVariant : BaseEntity
{
    public int Id {get; set;}
    public int ProductId {get; set;}
    public Product Product {get; set;}
    public string Name {get; set;}
    public int ColorId {get; set;}
    public Color Color {get; set;}
    public int SizeId {get; set;}
    public Size Size {get; set;}
    public decimal Price {get; set;}
    public int Stock {get; set;}
    
}