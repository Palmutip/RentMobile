using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RentMobile.Model.Login
{
    public class Token
    {
        [JsonPropertyName("access_token")]
        public string accessToken { get; set; }
        [JsonPropertyName("token_type")]
        public string tokenType { get; set; }
        [JsonPropertyName("expires_in")]
        public int expiresIn { get; set; }
    }
}
