﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CitelTeste.WebAppCitelTeste.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Descricao é obrigatório")]
        [StringLength(maximumLength: 50, ErrorMessage = "máximo 50 caractere.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Saldo é obrigatório")]
        public decimal Saldo { get; set; }

        public decimal Preco { get; set; }

        public DateTime? dataCriacao { get; set; }

        public int? CategoriaId { get; set; }
        
        public Categoria? Categoria { get; set; }
    }
}
