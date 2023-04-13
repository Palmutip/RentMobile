using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RentMobile.API.Model;
using RentMobile.Model.Login;

namespace RentMobile.API.Controler
{
    public interface IConsumoApi
    {
        HttpResponseMessage Request(string link, HttpMethod tipo, string token, string body);
    }

    public class ConsumoApi : IConsumoApi
    {
        public HttpResponseMessage Request(string link, HttpMethod tipo, string token, string body)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            HttpResponseMessage response = new HttpResponseMessage();

            if(token != null)
                request.Headers.Add("Authorization", "Bearer " + token);

            if (tipo == HttpMethod.Get)
            {
                link += string.Concat("?", body);
            }
            else
            {
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            request.RequestUri = new Uri(link);
            request.Method = tipo;


            return client.SendAsync(request).Result;


        }

        public HttpResponseMessage RequestLogin(string link, HttpMethod tipo, string usuario, string senha)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();

            var s = new StringContent($"username={usuario}&password={senha}&grant_type=password", 
                Encoding.UTF8, "application/x-www-form-urlencoded");

            request.RequestUri = new Uri(link);

            return client.PostAsync(link, s).Result;

        }

    }

}
