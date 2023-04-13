using RentMobile.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnunciarOpcoesPage : ContentPage
	{
		public AnunciarOpcoesPage ()
		{
			InitializeComponent ();

            if (!TokenStatic.isLoged())
                Navigation.PushModalAsync(new LoginPage());
        }

        private void MostraAnunciarNovo(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AnunciarNovoPage());
        }
    }
}