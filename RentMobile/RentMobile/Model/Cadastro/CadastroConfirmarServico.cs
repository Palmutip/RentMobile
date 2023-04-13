using Newtonsoft.Json;
using RentMobile.API;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace RentMobile.Model.Cadastro
{
    public class CadastroConfirmarServico
    {
        ApiServico apiServico { get; set; }

        private string rotaConfirmarCadastro = ApiHelper.MontarLink("/api/Admin/CONFIRMAR_CADASTRO");

        public void ConfirmarCadastro(CadastroConfirmarModel dados)
        {
            apiServico = new ApiServico();
            HttpResponseMessage response = apiServico.Executar(rotaConfirmarCadastro, HttpMethod.Post, JsonConvert.SerializeObject(dados));

            if (!response.IsSuccessStatusCode)
            {
                string mensagem = JsonDocument.Parse(response.Content.ReadAsStringAsync().Result).RootElement.GetProperty("message").ToString();
                throw new Exception(mensagem);
            }
        }
    }
}
