using System;
using System.Collections.Generic;
using System.Text;

namespace RentMobile.API
{
    public class RotasApi
    {
        //Login
        public static string Login { get {return ApiHelper.MontarLink("/api/token"); } }

        //Pessoa
        public static string GetCredencial { get { return ApiHelper.MontarLink("/api/Admin/GET_CREDENCIAL"); } } 

        //Anuncio Imagem
        public static string SavAnuncioImagem { get { return ApiHelper.MontarLink("/api/Aluguel/SAV_ANUNCIOIMAGEM"); } }
        public static string GetAnuncioImagem { get { return ApiHelper.MontarLink("/api/Aluguel/GET_ANUNCIOIMAGENS"); } }

        //Anuncio
        public static string GetAnuncio { get { return ApiHelper.MontarLink("/api/Aluguel/GET_ANUNCIO"); } }
        public static string SavAnuncio { get { return ApiHelper.MontarLink("/api/Aluguel/SAV_ANUNCIO"); } }

    }
}
