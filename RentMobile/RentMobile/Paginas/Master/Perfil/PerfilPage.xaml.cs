using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentMobile.Paginas.Master.Pagamento;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentMobile.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PerfilPage : ContentPage
	{
		public PerfilPage ()
		{
			InitializeComponent ();
		}

        private void BtnRetornar_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new HomePage());
        }

        private void BtnPagamentos_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PagamentosPage());
        }
    }
}