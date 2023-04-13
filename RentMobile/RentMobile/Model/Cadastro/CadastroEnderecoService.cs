using Newtonsoft.Json;
using RentMobile.API;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;

namespace RentMobile.Model.Cadastro
{
    public class CadastroEnderecoService
    {
        ApiServico apiServico { get; set; }

        private string rotaSavEndereco = ApiHelper.MontarLink("/api/Admin/SAV_PESSOAENDERECO");
        //

        public void CadastrarEndereco(CadastroEnderecoModel endereco)
        {
            apiServico = new ApiServico();
            HttpResponseMessage response = apiServico.Executar(rotaSavEndereco, HttpMethod.Post, JsonConvert.SerializeObject(endereco));

            if (!response.IsSuccessStatusCode)
            {
                string mensagem = (string)JsonObject.Parse(response.Content.ReadAsStringAsync().Result)["message"];
                throw new Exception(mensagem);
            }

        }


    }
}
