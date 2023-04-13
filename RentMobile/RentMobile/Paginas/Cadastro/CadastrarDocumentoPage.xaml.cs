using RentMobile.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.IO;
//using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CadastrarDocumentoPage : ContentPage
	{
        string imagemUsuario = null;

        public CadastroCredencialImagensService credencialImagensServico { get; set; }
		public CadastrarDocumentoPage ()
		{
			InitializeComponent ();
		}

        private void Retorna(object sender, EventArgs args)
        {
            Navigation.PopModalAsync();
        }

        private void AvancaCadastroConfirmacao(object sender, EventArgs args)
        {
            if (imagemUsuario != null)
            {
                CadastroCredencialImagensModel imagem = new CadastroCredencialImagensModel
                {
                   
                };

                credencialImagensServico = new CadastroCredencialImagensService();
                //credencialImagens.SavImagem(imagem);

                Navigation.PushModalAsync(new MasterPage(new CadastrarConfirmacaoPage()));
            }
            else
            {
                retornoMensagem.IsVisible = true;
            }
        }

        public async void TirarFoto(object sender, EventArgs args)
        {
            try
            {
                var foto = await MediaPicker.CapturePhotoAsync();

                imagemUsuario = Convert.ToBase64String(File.ReadAllBytes(foto.FullPath));

                ImagemUsuario.Source = ImageSource.FromFile(foto.FullPath);

                if (imagemUsuario != null)
                {
                    retornoMensagem.IsVisible = false;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                //
            }
        }
    }
}