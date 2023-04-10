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
            return await _context.Produto.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produto.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> PutProduto(int id, [FromForm] Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return produto;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
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
