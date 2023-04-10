using FirstApi.Data.DAL;
using FirstApi.Dtos.ProductDto;
using FirstApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(int page , string search)
        {
            var query = _context.Products
                .Where(p => !p.IsDelete);

            ProductListDto productListDto = new();
            productListDto.TotalCount = query.Count();
            productListDto.CurrentPage = page;


            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            productListDto.Items = query.Skip((page-1)*2)
                .Take(2)
                .Select(p => new ProductListItemDto
            {
                Name = p.Name,
                SalePrice = p.SalePrice,
                CreateDate = p.CreateDate,
                CostPrice = p.CostPrice,
                UpdateDate = p.UpdateDate
            }).ToList();

            List<ProductListItemDto> productListItemDtos = new();



            return StatusCode(200, productListDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Product product = _context.Products
                .Where(p => !p.IsDelete)
                .FirstOrDefault(x => x.Id == id);
            if (product == null) return StatusCode(StatusCodes.Status404NotFound);
            ProductReturnDto productReturnDto = new()
            {
                Name = product.Name,
                SalePrice = product.SalePrice,
                CostPrice = product.CostPrice,
                CreateDate = product.CreateDate,
                UpdateDate = product.UpdateDate
            };

            return Ok(productReturnDto);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductCreateDto productCreateDto)
        {
            Product newproduct = new()
            {
                Name = productCreateDto.Name,
                SalePrice = productCreateDto.SalePrice,
                CostPrice = productCreateDto.CostPrice,
                IsActive = productCreateDto.IsActive
            };
            _context.Products.Add(newproduct);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, newproduct);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var existProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();
            existProduct.Name = productUpdateDto.Name;
            existProduct.SalePrice = productUpdateDto.SalePrice;
            existProduct.CostPrice = productUpdateDto.CostPrice;
            existProduct.IsActive = productUpdateDto.IsActive;
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPatch]
        public IActionResult ChangeStatus(int id, bool IsActive)
        {
            //existProduct=_context
            var existProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();
            existProduct.IsActive = IsActive;
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
