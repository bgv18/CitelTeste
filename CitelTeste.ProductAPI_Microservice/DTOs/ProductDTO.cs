using CitelTeste.ProductAPI_Microservice.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CitelTeste.ProductAPI_Microservice.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatorio")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Preco obrigatorio")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Descricao obrigatoria")]
        [MinLength(5)]
        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Estoque obrigatorio")]
        [Range(1, 9999)]
        public long Stock { get; set; }

        public string? ImageURL { get; set; }
        public string? CategoryName { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
