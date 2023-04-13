using RentMobile.API;
using RentMobile.API.Controler;
using RentMobile.API.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RentMobile.Model.Login
{
    public class LoginModel
    {
        public string usuario { get; set; }
        public string senha { get; set; }
        public string grant_type { get; set; }

    }
}
