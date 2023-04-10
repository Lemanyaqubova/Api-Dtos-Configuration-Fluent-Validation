using FirstApi.Data.DAL;
using FirstApi.Dtos.CategoryDto;
using FirstApi.Dtos.ProductDto;
using FirstApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(int page, string search)
        {
            var query = _context.Categories
                .Where(p => !p.IsDelete);

            CategoryListDto categoryListDto = new();
            categoryListDto.TotalCount = query.Count();
            categoryListDto.CurrentPage = page;


            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }
            //if (category == null) return StatusCode(StatusCodes.Status404NotFound);
            categoryListDto.Items = query.Skip((page - 1) * 2)
                .Take(2)
                .Select(p => new CategoryListItemDto
                {
                    Name = p.Name,
                     Description= p.Description,
                    CreateDate = p.CreateDate,
                    UpdateDate = p.UpdateDate
                }).ToList();

            List<CategoryListItemDto> categoryListItemDtos = new();



            return StatusCode(200, categoryListDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Category category = _context.Categories
                .Where(p => !p.IsDelete)
                .FirstOrDefault(x => x.Id == id);
            if (category == null) return StatusCode(StatusCodes.Status404NotFound);
            CategoryReturnDto categoryReturnDto = new()
            {
                Name = category.Name,
                Description = category.Description,
                CreateDate = category.CreateDate,
                UpdateDate = category.UpdateDate
            };

            return Ok(categoryReturnDto);
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryCreateDto categoryCreateDto)
        {
            Category newcategory = new()
            {
                Name = categoryCreateDto.Name,
                Description = categoryCreateDto.Description,
                
            };
            _context.Categories.Add(newcategory);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, newcategory);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var existCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (existCategory == null) return NotFound();
            existCategory.Name = categoryUpdateDto.Name;
            existCategory.Description = categoryUpdateDto.Description;
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPatch]
        public IActionResult ChangeStatus(int id, string Description)
        {
            var existCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (existCategory == null) return NotFound();
            existCategory.Description = Description;
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }

    }
}
