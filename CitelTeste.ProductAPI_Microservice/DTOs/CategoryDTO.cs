using CitelTeste.ProductAPI_Microservice.Models;
using System.ComponentModel.DataAnnotations;

namespace CitelTeste.ProductAPI_Microservice.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
