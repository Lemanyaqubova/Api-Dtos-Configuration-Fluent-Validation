using FirstApi.Models;

namespace FirstApi.Dtos.ProductDto
{
    public class ProductListDto
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public List<ProductListItemDto> Items { get; set; }
    }
}
