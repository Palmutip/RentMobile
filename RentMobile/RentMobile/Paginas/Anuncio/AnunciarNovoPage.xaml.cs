using RentMobile.Model.Anuncio;
using RentMobile.Model.Login;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnunciarNovoPage : ContentPage
    {
        private Dictionary<string, string> ListaImagens = new Dictionary<string, string>();
        public AnuncioServico AnuncioServico { get; set; }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (tempoMesAnuncio.Items.Count < 1)
            {
                PreencherMesesDias();
            }

        }

        public AnunciarNovoPage()
        {
            InitializeComponent();

            BtnAvancar.Clicked += Avancar;
            BtnRetornar.Clicked += Retornar;
            btnProdDefeitoSim.Clicked += BtnSimClick;
            btnProdDefeitoNao.Clicked += BtnNaoClick;

        }

        private void btnMenuCustom_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(EventArgs.Empty, "OpenMenu");
        }

        public async void InserirImagemClick(object sender, EventArgs e)
        {
            var foto = await MediaPicker.PickPhotoAsync();
            var novaImagem = new Image();
            novaImagem.HeightRequest = 83;
            novaImagem.WidthRequest = 123;
            novaImagem.Source = ImageSource.FromFile(foto.FullPath);

            ListaImagens.Add(novaImagem.Id.ToString(), Convert.ToBase64String(File.ReadAllBytes(foto.FullPath)));

            StackImagens.Children.Add(novaImagem);

        }

        private async void Avancar(object sender, EventArgs e)
        {
            if (ValidarCamposSemErro())
            {
                load.IsRunning = true;

                if(await SalvarAnuncio())
                    await Navigation.PushModalAsync(new MasterPage());

                load.IsRunning = false;
            }
        }

        private void Retornar(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void BtnSimClick(object sender, EventArgs e)
        {
            btnProdDefeitoSim.BorderColor = Color.FromHex("#F5A119");
            btnProdDefeitoSim.TextColor = Color.FromHex("#F5A119");
            btnProdDefeitoSim.BorderWidth = 1;
            btnProdDefeitoSim.BackgroundColor = Color.Transparent;

            btnProdDefeitoNao.BorderColor = Color.White;
            btnProdDefeitoNao.TextColor = Color.White;
            btnProdDefeitoNao.BorderWidth = 1;
            btnProdDefeitoNao.BackgroundColor = Color.Transparent;

            frameDetalheProduto.IsVisible = true;
        }

        private void BtnNaoClick(object sender, EventArgs e)
        {
            btnProdDefeitoNao.BorderColor = Color.FromHex("#F5A119");
            btnProdDefeitoNao.TextColor = Color.FromHex("#F5A119");
            btnProdDefeitoNao.BorderWidth = 1;
            btnProdDefeitoNao.BackgroundColor = Color.Transparent;

            btnProdDefeitoSim.BorderColor = Color.White;
            btnProdDefeitoSim.TextColor = Color.White;
            btnProdDefeitoSim.BorderWidth = 1;
            btnProdDefeitoSim.BackgroundColor = Color.Transparent;

            detalheProduto.Text = null;
            frameDetalheProduto.IsVisible = false;
        }

        private async Task<bool> SalvarAnuncio()
        {

            AnuncioServico = new AnuncioServico();

            int dias = Convert.ToInt32(tempoDiaAnuncio.SelectedItem);
            int meses = Convert.ToInt32(tempoMesAnuncio.SelectedItem);

            AnuncioModel anuncio = new AnuncioModel
            {
                titulo = nomeAnuncio.Text,
                descricao = descricaoAnuncio.Text,
                catgoria = catergoriaAnuncio.SelectedIndex,
                produtoComDefeito = detalheProduto.IsVisible ? true : false,
                descricaoDefeito = detalheProduto.Text,
                valorAnuncio = Convert.ToDecimal(valorAnuncio.Text),
                tempoAluguel = DateTime.Now.AddMonths(meses).AddDays(dias).AddHours(1).ToString("u").Substring(0, 19),
                pessoa = Convert.ToInt64(TokenStatic.handlePessoa)
            };

            var imagens = new List<AnuncioImagensModel>();

            foreach (var imagem in ListaImagens)
            {
                var imagenAtual = new AnuncioImagensModel();

                imagens.Add(new AnuncioImagensModel { imagem = imagem.Value });
            }

            try
            {

                await Task.Run(() => AnuncioServico.CadastrarAnuncio(anuncio, imagens));
            }            
            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    retornoMensagem.Text = ex.Message;
                    retornoMensagem.IsVisible = true;
                }

                return false;
            }
            catch (Exception e)
            {
                retornoMensagem.Text = e.Message;
                retornoMensagem.IsVisible = true;

                return false;
            }

            return true;

        }

        private void PreencherMesesDias()
        {
            tempoMesAnuncio.Items.Add("0");

            for (int i = 1; i < 31; i++)
            {
                tempoDiaAnuncio.Items.Add(i.ToString());

                if (i < 13)
                    tempoMesAnuncio.Items.Add(i.ToString());
            }

           
        }

        private bool ValidarCamposSemErro()
        {
            int erros = 0;

            retornoDesricaoAnuncio.IsVisible = false;
            retornoDetalheProduto.IsVisible = false;
            retornoNomeAnuncio.IsVisible = false;
            retornoTempoAluguel.IsVisible = false;
            retornoValorAnuncio.IsVisible = false;
            retornoFotos.IsVisible = false;

            if(StackImagens.Children.Count < 3)
            {
                erros++;
                retornoFotos.IsVisible = true;
            }

            if(nomeAnuncio.Text == null)
            {
                retornoNomeAnuncio.IsVisible = true;
                erros++;
            }

            if(descricaoAnuncio.Text == null)
            {
                retornoDesricaoAnuncio.IsVisible = true;
                erros++;
            }

            if(detalheProduto.IsVisible && detalheProduto.Text == null)
            {
                retornoDetalheProduto.IsVisible = true;
                erros++;
            }

            if(valorAnuncio.Text == null)
            {
                retornoValorAnuncio.IsVisible = true;
                erros++;
            }

            if(tempoDiaAnuncio.SelectedIndex == -1 || tempoMesAnuncio.SelectedIndex == -1)
            {
                retornoTempoAluguel.IsVisible = true;
                erros++;
            }

            return erros > 0 ? false : true;

        }
    }
}