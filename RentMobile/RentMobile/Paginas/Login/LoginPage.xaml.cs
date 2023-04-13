using RentMobile.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using static Java.Util.ResourceBundle;

namespace RentMobile
{
    public partial class LoginPage : ContentPage
    {
        ILoginService loginService { get; set; }

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void ValidarCredenciais(object sender, EventArgs args)
        {
            var loginService = new LoginService();

            if (string.IsNullOrEmpty(EntryEmail.Text) || string.IsNullOrWhiteSpace(EntryEmail.Text)
                 || string.IsNullOrEmpty(EntrySenha.Text) || string.IsNullOrWhiteSpace(EntrySenha.Text)
                )
                MostrarErro();
            else
            {
                load.IsRunning = true;
                if (await Task.Run<bool>(() => loginService.Login(EntryEmail.Text, EntrySenha.Text)))
                {
                    OcultarErro();

                    App.Current.MainPage = new MasterPage();
                }
                else
                {
                    MostrarErro();
                }
                load.IsRunning = false;
            }

        }

        private void OnTapped(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new MasterPage(new CadastrarPessoaPage()));
        }

        private void MostrarErro()
        {
            RetornoLogin.IsVisible = true;
            RetornoLogin.Text = "* Usuario/Senha incorretos";
        }

        private void OcultarErro()
        {
            RetornoLogin.IsVisible = false;
        }

        private void EntryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
