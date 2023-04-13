using CitelTeste.ProductAPI_Microservice.Models;
using Microsoft.EntityFrameworkCore;

namespace CitelTeste.ProductAPI_Microservice.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
