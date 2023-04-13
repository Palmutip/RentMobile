using RentMobile.API;
using RentMobile.API.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace RentMobile.Model.Cadastro
{
    public interface ICadastroUsuarioService
    {

    }

    public class CadastroUsuarioService
    {
        ApiServico apiServico { get; set; }

        private string rotaCadatroUsuario = ApiHelper.MontarLink("/api/Public/CADASTRA_NOVO_USUARIO");
        //
        public void CadastrarUsuario(CadastroUsuarioModel cadastrUsuario)
        {
            apiServico = new ApiServico();
            HttpResponseMessage response = apiServico.Executar(rotaCadatroUsuario, HttpMethod.Post, JsonConvert.SerializeObject(cadastrUsuario), false);

            if (!response.IsSuccessStatusCode)
            {
                string mensagem = (string)JObject.Parse(response.Content.ReadAsStringAsync().Result)["message"];

                //string mensagem = JsonConvert.de JsonDocument.Parse(response.Content.ReadAsStringAsync().Result).RootElement.GetProperty("message").ToString();
                throw new Exception(mensagem);  
            }
            //system.text.json utf8jsonwriter failed

        }

    }
}
