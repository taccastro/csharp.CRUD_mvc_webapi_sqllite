using System;
using System.ComponentModel.DataAnnotations;

namespace AnuncioWeb.Models
{
    public class Anuncio
    {

        public int id { get; set; }

        [Required]
        [MaxLength(45)]
        public string marca { get; set; }

        [Required]
        [MaxLength(45)]
        public string modelo { get; set; }

        [Required]
        [MaxLength(45)]
        public string versao { get; set; }
        
        [Required]
        public int ano { get; set; }
        
        [Required]
        public int quilometragem { get; set; }
        
        [Required]
        public string observacao { get; set; }

        public bool Ativo { get; set; }
        
        public DateTime Criado { get; set; }

        public DateTime? Atualizado { get; set; }




    }
}
