using System;

namespace AnuncioWeb.Models.DTO
{
    public class AnuncioDTO : BaseDTO
    {
        public int id { get; set; }

        public string marca { get; set; }

        public string modelo { get; set; }

        public string versao { get; set; }
               
        public int ano { get; set; }
               
        public int quilometragem { get; set; }
               
        public string observacao { get; set; }

        public bool Ativo { get; set; }

        public DateTime Criado { get; set; }

        public DateTime? Atualizado { get; set; }

    }
}
