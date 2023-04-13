using RentMobile.API;
using RentMobile.API.Controler;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xamarin.Essentials;

namespace RentMobile.Model.Login
{
    public interface ILoginService
    {
        bool Login(string usuario, string senha);
    }

    public class LoginService : ILoginService
    {
        public ConsumoApi consumoApi { get; set; }
        public ApiServico apiServico { get; set; }


        public bool Login(string usuario, string senha)
        {
            consumoApi = new ConsumoApi();

            LoginModel login = new LoginModel
            {
                usuario = usuario,
                senha = senha,
                grant_type = "password"
            };
            
            var retorno = consumoApi.RequestLogin(RotasApi.Login, HttpMethod.Post, usuario, senha);

            if (retorno.IsSuccessStatusCode)
            {
                Token token = JsonSerializer.Deserialize<Token>(retorno.Content.ReadAsStringAsync().Result);
                TokenStatic.accessToken = token.accessToken;
                TokenStatic.expiresIn = token.expiresIn;
                TokenStatic.tokenType = token.tokenType;

                apiServico = new ApiServico();
                var credencial = apiServico.Executar(RotasApi.GetCredencial, HttpMethod.Get, $"email = {usuario}");

                if (credencial.IsSuccessStatusCode)
                {
                    var raiz = JsonDocument.Parse(credencial.Content.ReadAsStringAsync().Result.Replace("[", "").Replace("]", "")).RootElement.GetProperty("dados").ToString();
                    var res = JsonDocument.Parse(raiz).RootElement;

                    TokenStatic.email = usuario;
                    //TokenStatic.senha = senha;
                    TokenStatic.nome = res.GetProperty("nome").ToString();
                    TokenStatic.handlePessoa = res.GetProperty("pessoa").ToString();
                    TokenStatic.enderecoCadastrado = res.GetProperty("enderecocadastrado").ToString();
                    TokenStatic.documentoInseriodo = res.GetProperty("documentoinserido").ToString();

                    Preferences.Set("UserName", login.usuario);
                    Preferences.Set("UserPass", login.senha);
                }
                else
                {
                    TokenStatic.Logoff();
                }


                return true;
            }

            return false;



        }
    }
}
