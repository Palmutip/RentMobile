using RentMobile.Model.Cadastro;
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
	public partial class CadastrarConfirmacaoPage : ContentPage
	{
        public CadastroConfirmarModel CadModel { get; set; }
        public CadastroConfirmarServico CadService { get; set; }
		public CadastrarConfirmacaoPage ()
		{
			InitializeComponent ();
		}

        private async void ConfirmarAvancar(object sender, EventArgs args)
        {
            retornoMensagem.IsVisible = false;

            if (CodigoValido())
            {
                CadModel = new CadastroConfirmarModel();
                CadService = new CadastroConfirmarServico();

                CadModel.email = TokenStatic.email;
                CadModel.chave = String.Concat(Cod1.Text, Cod2.Text, Cod3.Text, Cod4.Text, Cod5.Text);



                load.IsRunning = true;
                try
                {
                    await Task.Run(() => CadService.ConfirmarCadastro(CadModel));
                    
                    App.Current.MainPage = new MasterPage();
                }
                catch (AggregateException ae)
                {
                    foreach (var ex in ae.InnerExceptions)
                    {
                        retornoMensagem.Text = ex.Message;
                        retornoMensagem.IsVisible = true;
                    }
                }
                catch (Exception e)
                {
                    retornoMensagem.Text = e.Message;
                }

                load.IsRunning = false;
            }
            else
            {
                retornoMensagem.IsVisible = true;
            }
            
 
                       
        }

        private async void Retorna(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        public void ProximoCampo(object sender, EventArgs args)
        {
            var entry = (Entry)sender;

            if(entry.Text == "")
            {
                switch (entry.ClassId)
                {
                    case "2": Cod1.Focus(); break;
                    case "3": Cod2.Focus(); break;
                    case "4": Cod3.Focus(); break;
                    case "5": Cod4.Focus(); break;

                }
            }
            else
            {
                switch (entry.ClassId)
                {
                    case "1": Cod2.Focus(); break;
                    case "2": Cod3.Focus(); break;
                    case "3": Cod4.Focus(); break;
                    case "4": Cod5.Focus(); break;

                }
            }

            
        }
                

        public bool CodigoValido()
        {
            if (Cod1.Text == null)
                return false;
            if (Cod2.Text == null)
                return false;
            if (Cod3.Text == null)
                return false;
            if (Cod4.Text == null)
                return false;
            if (Cod5.Text == null)
                return false;

            return true;
        }
    }
}