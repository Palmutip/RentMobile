using RentMobile.API;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace RentMobile.Model.Cadastro
{
    public class CadastroCredencialImagensService
    {
        ApiServico apiServico { get; set; }

        private string rotaSavCredencialImagens = ApiHelper.MontarLink("/api/Admin/SAV_CREDENCIALIMAGENS");
        //

        public void SavImagem(CadastroCredencialImagensModel imagem)
        {
            //HttpResponseMessage response = apiServico.Executar(rotaSavCredencialImagens, HttpMethod.Post, imagem);

            //if (!response.IsSuccessStatusCode)
            //{
            //    string mensagem = JsonDocument.Parse(response.Content.ReadAsStringAsync().Result).RootElement.GetProperty("message").ToString();
            //    throw new Exception("mensagem");
            //}

        }
    }
}
