using RentMobile.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
    {
        private bool _estado = false;
		public HomePage ()
		{            
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            
            //if (TokenStatic.isLoged()) 
            //{ 
            //    var teste = TokenStatic.enderecoCadastrado; 
            //}
        }

        private void MostraAnunciarOpcoes(object sender, EventArgs args)
        {
            //App.Current.MainPage = new NavigationPage(new AnunciarOpcoesPage());
            Navigation.PushModalAsync(new MasterPage(new AnunciarOpcoesPage()));
            //new NavigationPage(new AnunciarOpcoesPage());
        }

        public void BannerClick()
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MasterPage(new AlugarListagemPage()));
        }
    }
}