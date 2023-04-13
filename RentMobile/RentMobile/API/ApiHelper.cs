using System;
using System.Collections.Generic;
using System.Text;

namespace RentMobile.API
{
    public abstract class ApiHelper
    {
        //public const string LINK = "http://palmutip.com.br:7502";
        public const string LINK = "http://192.168.56.1:7502";


        public static string MontarLink(string rota)
        {
            return LINK + rota;
        }
    }
}
