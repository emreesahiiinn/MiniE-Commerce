using Core.Entities.Abstract;

namespace Entities.DTOs;

public class ProductDetailDto : IDto
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public int UnitsInStock { get; set; }
}