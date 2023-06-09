﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CitelTeste.WebAppCitelTeste.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(maximumLength: 50, ErrorMessage = "máximo 50 caractere.")]
        public string? Nome { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }
    }
}
