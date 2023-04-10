using CitelTeste.ProdutoApi.data;
using CitelTeste.ProdutoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CitelTesteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly BancoContext _context;

        public ProdutoController(BancoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProduto()
        {
            return Ok(await _context.Produto.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            if(id == 0)
                return BadRequest("Informe um produto");

            var produto = await _context.Produto.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            if (produto == null)
                return BadRequest("Produto nao informado");

            if(produto.CategoriaId == 0)
                return BadRequest("Categoria do produto nao informada");

            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> PutProduto(int id, [FromForm] Produto produto)
        {
            if (id == 0)
                return BadRequest("Informe o id do produto");

            if (produto == null)
                return BadRequest("Produto nao informado");

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            if (id == 0)
                return BadRequest("Informe o id do produto");

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
