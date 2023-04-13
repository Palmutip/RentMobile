using System;
using System.Collections.Generic;
using System.Text;

namespace RentMobile.Model.Anuncio
{
    public class AnuncioModel
    {
        public long handle { get; set; }
        public decimal valorAnuncio { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public int catgoria { get; set; }
        public string descricaoDefeito { get; set; }
        public bool produtoComDefeito { get; set; }
        public string tempoAluguel { get; set; }
        public long pessoa { get; set; }

    }
}
