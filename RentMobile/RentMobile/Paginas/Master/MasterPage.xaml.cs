using RentMobile.Model.Login;
using RentMobile.Paginas;
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
	public partial class MasterPage : FlyoutPage
    {
        //public bool aberto = false;
		public MasterPage ()
		{
			InitializeComponent ();

            HomePage home = new HomePage ();
            home.Appearing += Home_Appearing;

            Home_Appearing(home, new EventArgs());

            controlaMenuLogin();

            Detail = new NavigationPage(new HomePage());
            
            MessagingCenter.Subscribe<EventArgs>(this, "OpenMenu", args =>
            {
                IsPresented = !IsPresented;
            });
                       
        }

        public MasterPage(Page detale)
        {
            InitializeComponent();
            controlaMenuLogin();

            Detail = new NavigationPage(detale);

            MessagingCenter.Subscribe<EventArgs>(this, "OpenMenu", args =>
            {
                IsPresented = !IsPresented;
            });
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //controlaMenuLogin();
        }
        void GoPerfil(object sender, EventArgs args)
        {
           App.Current.MainPage = new NavigationPage(new PerfilPage());
        }

        void GoHome(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
        }

        void GoCarrinho(object sender, EventArgs args)
        {
            //App.Current.MainPage = new NavigationPage(new );
        }

        void GoConfiguracoes(object sender, EventArgs args)
        {
            //App.Current.MainPage = new NavigationPage()
        }

        void Sair(object sender, EventArgs args)
        {
            TokenStatic.Logoff();
            Xamarin.Essentials.Preferences.Clear();
            controlaMenuLogin();
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
        }

        void Entrar(object sender, EventArgs args)
        {
            ///Detail = new NavigationPage(new LoginPage());
            Detail = new NavigationPage(new LoginPage());
            IsPresented = false;
        }

        private async void controlaMenuLogin()
        {
            
            if (Xamarin.Essentials.Preferences.Get("UserPass", "") != "")
            {
                var loginService = new LoginService();
                if (await Task.Run<bool>(() => loginService.Login(Xamarin.Essentials.Preferences.Get("UserName", ""), Xamarin.Essentials.Preferences.Get("UserPass", ""))))
                    loginService.Login(Xamarin.Essentials.Preferences.Get("UserName", ""), Xamarin.Essentials.Preferences.Get("UserPass", ""));
            }
            
            if (TokenStatic.isLoged())
            {
                entrar.IsVisible = false;                
                perfil.IsVisible = true;
                sair.IsVisible = true;

                imagemPerfil.ImageSource = "drawable/ImagemPerfil.png";
                nomeUsuario.Text = TokenStatic.nome;
                mensagem.Text = "Editar minhas informações";
            }
            else
            {
                entrar.IsVisible = true;
                perfil.IsVisible = false;
                sair.IsVisible = false;

                imagemPerfil.ImageSource = "drawable/ImagemPerfil.png";
                nomeUsuario.Text = "Seja bem-vindo ";
                mensagem.Text = "Clique abaixo para logar";
            }
        }

        private void Home_Appearing(object sender, EventArgs e)
        {
            if (TokenStatic.isLoged())
            {
                var teste = TokenStatic.enderecoCadastrado;
            }

            RentMobile.View.Menu menu = (sender as ContentPage).FindByName("ViewMenu") as RentMobile.View.Menu;

            menu.LblEndereco.Text = "Teste de Endereço";
            var asdasd = menu.LblEndereco;
        }
    }
}