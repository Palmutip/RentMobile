using System;
using System.Collections.Generic;
using System.Text;

namespace RentMobile.Model.Cadastro
{
    public class CadastroCredencialImagensModel
    {
        public long handle { get; set; }
        public long? credencial { get; set; }
        public long tipodocumento { get; set; }
        public string imagem { get; set; }
    }
}
