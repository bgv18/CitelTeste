using CitelTeste.ProdutoApi.Models;
using CitelTesteApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CitelTeste.ProdutoApi.data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
            
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}
