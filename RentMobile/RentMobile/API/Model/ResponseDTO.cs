using System;
using System.Collections.Generic;
using System.Text;

namespace RentMobile.API.Model
{
    public class ResponseDTO
    {
        public long ID { get; set; } //ID do parceiro
        public string Status { get; set; } //Status [S]Sucesso ou [E]Erro
        public string Dados { get; set; } //Dados obtidos
        public string Mensagem { get; set; } //Texto do retorno, quando houver erros
        public string DataHora { get; set; } //Data e hora da geração do retorno
    }
}
