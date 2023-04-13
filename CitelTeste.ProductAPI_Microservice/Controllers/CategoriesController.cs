using CitelTeste.ProductAPI_Microservice.DTOs;
using CitelTeste.ProductAPI_Microservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitelTeste.ProductAPI_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categoriesDto = await _categoryService.GetCategories();
            if(categoriesDto is null)
                return NotFound("Categorias nao encontradas.");
            
            return Ok(categoriesDto);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            var categoriesDto = await _categoryService.GetCategoriesProducts();
            if (categoriesDto is null)
                return NotFound("Categorias nao encontradas.");

            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get(int id)
        {
            var categoryDto = await _categoryService.GetCategoryById(id);
            if (categoryDto is null)
                return NotFound("Categoria nao encontrada.");

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto is null)
                return BadRequest("Dados invalidos");

            await _categoryService.AddCategory(categoryDto);

            return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id) 
                return BadRequest("Id difere entre os dados.");

            if (categoryDto is null)
                return BadRequest("Dados invalidos");

            await _categoryService.UpdateCategory(categoryDto);

            return Ok(categoryDto);
        }

        [HttpDelete("id:int")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var categoryDto = await _categoryService.GetCategoryById(id);
            if (categoryDto is null)
                return NotFound("Categoria nao encontrada");

            await _categoryService.RemoveCategory(id);
            return Ok(categoryDto);
        }
    }
}
