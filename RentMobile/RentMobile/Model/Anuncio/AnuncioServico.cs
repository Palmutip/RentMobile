using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RentMobile.API;
using RentMobile.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RentMobile.Model.Anuncio
{
    public class AnuncioServico
    {
        public ApiServico apiServico { get; set; }

        public AnuncioServico()
        {
            apiServico = new ApiServico();
        }

        public void CadastrarAnuncio(AnuncioModel anuncio, List<AnuncioImagensModel> imagens)
        {
            apiServico = new ApiServico();
            HttpResponseMessage response = apiServico.Executar(RotasApi.SavAnuncio, HttpMethod.Post, JsonConvert.SerializeObject(anuncio));

            if (response.IsSuccessStatusCode)
            {
                string dados = (string)JObject.Parse(response.Content.ReadAsStringAsync().Result)["dados"];
                long handle = Convert.ToInt64((string)JObject.Parse(response.Content.ReadAsStringAsync().Result)["handle"]);

                imagens.ForEach(x => x.anuncio = handle);

                imagens.ForEach(x => SalvarImagemAnuncio(x));
                
            }
            else
            {
                string mensagem = (string)JObject.Parse(response.Content.ReadAsStringAsync().Result)["message"];

                //string mensagem = JsonConvert.de JsonDocument.Parse(response.Content.ReadAsStringAsync().Result).RootElement.GetProperty("message").ToString();
                throw new Exception(mensagem);
            }
            //system.text.json utf8jsonwriter failed

        }

        public void SalvarImagemAnuncio(AnuncioImagensModel imagem)
        {
            apiServico = new ApiServico();
            HttpResponseMessage response = apiServico.Executar(RotasApi.SavAnuncioImagem, HttpMethod.Post, JsonConvert.SerializeObject(imagem));

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
