using RentMobile.API.Controler;
using RentMobile.API.Model;
using RentMobile.Model.Login;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace RentMobile.API
{
    public class ApiServico
    {
        public string link { get; set; }
        public HttpMethod tipoMetodo { get; set; }
        public string body { get; set; }


        public IConsumoApi _consumoApi { get; set; }
        private string token { get; set; }


        public ApiServico() { }

        public ApiServico(string link, HttpMethod tipoMetodo, string body)
        {
            this.link = link;
            this.tipoMetodo = tipoMetodo;
            this.body = body;
        }

        public HttpResponseMessage Executar(string link, HttpMethod tipoMetodo, string body, bool IsNotPublic = true)
        {
            this.link = link;
            this.tipoMetodo = tipoMetodo;
            this.body = body;

            if (IsNotPublic)
            {
                token = TokenStatic.accessToken;
            }

            //Temporario, trocar por injeção de DP ou tornar metodo estatico
            _consumoApi = new ConsumoApi();

            return _consumoApi.Request(link, tipoMetodo, token, body);

        }

    }
}
