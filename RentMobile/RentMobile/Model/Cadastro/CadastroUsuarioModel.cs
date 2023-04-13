using System;
using System.Collections.Generic;
using System.Text;

namespace RentMobile.Model.Cadastro
{
    public class CadastroUsuarioModel
    {
        public int handle { get; set; }
        public string nome { get; set; }
        public string dtNascimento { get; set; }
        public string telefone { get; set; }
        public string rg { get; set; }
        public string cpf { get; set; }
        public string sexo { get; set; }
        public string estadocivil { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
    }
}
