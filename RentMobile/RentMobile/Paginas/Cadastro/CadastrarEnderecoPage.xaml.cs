using RentMobile.Model.Cadastro;
using RentMobile.Model.Login;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CadastrarEnderecoPage : ContentPage
	{
        public CadastroEnderecoService cadastroEnderecoService { get; set; }

        //ActivityIndicator load = new ActivityIndicator { Color = Color.Red, IsRunning = true};

        public CadastrarEnderecoPage ()
		{
			InitializeComponent ();
        }

        private async void Retorna(object sender, EventArgs args)
        {
           await Navigation.PopModalAsync();
        }

        private async void AvancaCadastroDocumento(object sender, EventArgs args)
        {
            retornoMensagem.IsVisible = false;
            if (ExisteCampoSemErro())
            {
                load.IsRunning = true;
                await RealizarCadastro();
                load.IsRunning = false;
                

            }

        }

        public async Task RealizarCadastro()
        {
            CadastroEnderecoModel endereco = new CadastroEnderecoModel
            {
                logradouro = logradouro.Text,
                bairro = bairro.Text,
                numero = numero.Text,
                cep = cep.Text.Replace("-", ""),
                estado = estado.SelectedItem.ToString(),
                municipio = cidade.Text,
                pessoa = Convert.ToInt64(TokenStatic.handlePessoa)
            };

            cadastroEnderecoService = new CadastroEnderecoService();

            try
            {
                await Task.Run(() => cadastroEnderecoService.CadastrarEndereco(endereco));
                
                await Navigation.PushModalAsync(new MasterPage(new CadastrarDocumentoPage()));
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
        }

        public async void ValidaBuscarCep(object sender, EventArgs args)
        {
            if ((!String.IsNullOrEmpty(cep.Text) && cep.Text.Length == 9))
            {
                await BuscaCep(cep.Text.Replace("-", ""));
            }
        }

        public void ValidaCep(object sender, EventArgs args)
        {
            ValidoCep();    
        }
        private bool ValidoCep()
        {
            if (String.IsNullOrEmpty(cep.Text) || cep.Text.Length < 9)
            {
                cepMensagem.IsVisible = true;
                return false;
            }
            else
            {
                cepMensagem.IsVisible = false;
                return true;
            }
        }

        private async Task BuscaCep(string cep)
        {
            load.IsRunning = true;
            var client = new RestClient($"http://viacep.com.br/ws/{cep}/json/");
            
            var request = new RestRequest();

            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var obj = JsonDocument.Parse(response.Content);

                estado.SelectedItem = estado.Items.FirstOrDefault(x => x == obj.RootElement.GetProperty("uf").GetString());
                cidade.Text = obj.RootElement.GetProperty("localidade").GetString();
                logradouro.Text = obj.RootElement.GetProperty("logradouro").GetString();
                bairro.Text = obj.RootElement.GetProperty("bairro").GetString();

            }
            load.IsRunning = false;

        }

        public void ValidaEstado(object sender, EventArgs args)
        {
            ValidoEstado();
        }

        private bool ValidoEstado()
        {
            if (estado.SelectedIndex == -1)
            {
                estadoMensagem.IsVisible = true;
                return false;
            }
            else
            {
                estadoMensagem.IsVisible = false;
                return true;
            }
        }

        public void ValidaCidade(object sender, EventArgs args)
        {
            ValidoCidade();
        }

        private bool ValidoCidade()
        {
            if (string.IsNullOrEmpty(cidade.Text) || string.IsNullOrWhiteSpace(cidade.Text) || cidade.Text.Length < 3)
            {
                cidadeMensagem.IsVisible = true;
                return false;
            }
            else
            {
                cidadeMensagem.IsVisible = false;
                return true;
            }
        }

        public void ValidaLogradouro(object sender, EventArgs args)
        {
            ValidoLogradouro();
        }

        private bool ValidoLogradouro()
        {
            if (string.IsNullOrEmpty(logradouro.Text) || string.IsNullOrWhiteSpace(logradouro.Text) || logradouro.Text.Length < 3)
            {
                logradouroMensagem.IsVisible = true;
                return false;
            }
            else
            {
                logradouroMensagem.IsVisible = false;
                return true;
            }
        }

        public void ValidaBairro(object sender, EventArgs args)
        {
            ValidoBairro();
        }

        private bool ValidoBairro()
        {
            if (string.IsNullOrEmpty(bairro.Text) || string.IsNullOrWhiteSpace(bairro.Text) || bairro.Text.Length < 3)
            {
                bairroMensagem.IsVisible = true;
                return false;
            }
            else
            {
                bairroMensagem.IsVisible = false;
                return true;
            }
        }

        private void ValidaNumero(object sender, EventArgs args)
        {
            ValidaNumero();
        }
        private bool ValidaNumero()
        {            
            if (string.IsNullOrEmpty(numero.Text) || string.IsNullOrWhiteSpace(numero.Text))
            {
                numeroMensagem.IsVisible = true;
                return false;
            }

            numeroMensagem.IsVisible = false;
            return true;
            
        }

        private bool ExisteCampoSemErro()
        {
            if(!ValidoCep())
                return false;

            if (!ValidoEstado())
                return false;

            if (!ValidoCidade())
                return false;

            if (!ValidoLogradouro())
                return false;

            if (!ValidoBairro())
                return false;

            if (!ValidaNumero())
                return false;

            return true;

        }

    }
}