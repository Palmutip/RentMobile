using System;
using System.Collections.Generic;
using System.Text;

namespace RentMobile.Model.Login
{
    public static class TokenStatic
    {
        public static string accessToken { get; set; }
        public static string tokenType { get; set; }
        public static int expiresIn { get; set; }

        public static string email { get; set; }
        public static string senha { get; set; }
        public static string nome { get; set; }

        public static string handlePessoa { get; set; }
        public static string enderecoCadastrado { get; set; }
        public static string documentoInseriodo { get; set; }

        public static bool isLoged()
        {
           return accessToken != null ? true : false;
        }

        public static void Logoff()
        {
            accessToken = null;
            tokenType = null;
            expiresIn = 0;
            email = null;
            senha = null;
        }
    }
}
