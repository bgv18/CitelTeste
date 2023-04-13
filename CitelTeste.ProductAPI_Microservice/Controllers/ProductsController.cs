using CitelTeste.ProductAPI_Microservice.DTOs;
using CitelTeste.ProductAPI_Microservice.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitelTeste.ProductAPI_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var productsDto = await _productService.GetProducts();

            if(productsDto is null)
                return NotFound("Produtos nao encontrados");

            return Ok(productsDto);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var produtoDto = await _productService.GetProductById(id);

            if (produtoDto is null)
                return NotFound("Produto nao informado");

            return Ok(produtoDto);
        }

        [HttpPost]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id)
                return BadRequest("Dados diferem");

            if (productDto is null)
                return BadRequest("Dados nao informados");

            await _productService.UpdateProduct(productDto);
            return Ok(productDto);
        }

        [HttpDelete]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var produtoDto = await _productService.GetProductById(id);

            if (produtoDto is null)
                return NotFound("Produto nao encontrado");

            await _productService.RemoveProduct(id);

            return Ok(produtoDto);
        }
    }
}
