using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentMobile.Model.Cadastro
{
    public class CadastroEnderecoModel
    {
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string cep { get; set; }
        public string bairro { get; set; }
        public string municipio { get; set; }
        public string estado { get; set; }
        public long pais { get; set; }
        public string complemento { get; set; }
        public long pessoa { get; set; }
    }
}
