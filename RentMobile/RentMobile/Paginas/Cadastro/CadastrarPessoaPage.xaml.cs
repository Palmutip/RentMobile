using RentMobile.API;
using RentMobile.API.Controler;
using RentMobile.Model.Cadastro;
using RentMobile.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;

namespace RentMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CadastrarPessoaPage : ContentPage
	{
        public CadastroUsuarioService cadastroPessoaService { get; set; }
        public LoginService login { get; set; }

        public CadastrarPessoaPage ()
		{
			InitializeComponent ();
        }

        private async void Retorna(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        private async void AvancaCadastro2(object sender, EventArgs args)
        {
            retornoMensagem.IsVisible = false;

            try
            {

                if (!TokenStatic.isLoged() && ExisteCampoSemErro())
                {
                    load.IsRunning = true;

                    bool cadastrado = await RealizarCadastro();

                    load.IsRunning = false;

                    if (cadastrado == true && TokenStatic.isLoged())
                    {
                        await Navigation.PushModalAsync(new MasterPage(new CadastrarEnderecoPage()));
                    }
                    else if (cadastrado == true && !TokenStatic.isLoged())
                    {
                        await Navigation.PopModalAsync();
                    }

                }                

            }
            catch(Exception ex)
            {
                string log = ex.Message;

                retornoMensagem.Text = log;
            }      
        }

        private async Task<bool> RealizarCadastro()
        {
            string especiais = "[-./() ]";
            bool cadastrado = false;

            CadastroUsuarioModel pessoa = new CadastroUsuarioModel
            {
                nome = nome.Text + " " + sobrenome.Text,
                dtNascimento = dataNascimento.Text,
                cpf = Regex.Replace(cpf.Text, especiais, ""),
                email = email.Text,
                rg = rg.Text,
                estadocivil = estadocivil.SelectedItem.ToString().Substring(0, 1),
                sexo = genero.SelectedItem.ToString().Substring(0, 1),
                senha = senha.Text,
                telefone = Regex.Replace(telefone.Text, especiais, "")
            };

            cadastroPessoaService = new CadastroUsuarioService();

            login = new LoginService();

            try
            {
                await Task.Run(() => cadastroPessoaService.CadastrarUsuario(pessoa));
                
                cadastrado = true;

                await Task.Run(() => login.Login(pessoa.email, pessoa.senha));
                

            }
            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    retornoMensagem.Text = ex.Message;
                    retornoMensagem.IsVisible = true;
                }

                return cadastrado;
            }
            catch (Exception e)
            {
                retornoMensagem.Text = e.Message;
                retornoMensagem.IsVisible = true;

                return cadastrado;
            }

            return cadastrado;
        }

        #region VALIDAÇÃO DE CAMPOS
        public void ValidaNome(object sender, EventArgs args)
        {
            ValidaNome();
        }

        private bool ValidaNome()
        {
            if (nome.Text == null || nome.Text.Length == 0)
            {
                nomeMensagem.IsVisible = true;
                return true;
            }

           nomeMensagem.IsVisible = false;
            return false;
        }

        public void ValidaSobrenome(object sender, EventArgs args)
        {
            ValidaSobrenome();
        }

        private bool ValidaSobrenome()
        {
            if (sobrenome.Text == null || sobrenome.Text.Length == 0)
            {
                sobrenomeMensagem.IsVisible = true;
                return true;
            }
            
            sobrenomeMensagem.IsVisible = false;       

            return false;
        }

        public void ValidaDtNascimento(object sender, EventArgs args)
        {
            ValidaDtNascimento();
        }

        private bool ValidaDtNascimento()
        {
            dataNascimentoMensagem.Text = "* Informe uma Data Valida";


            if (string.IsNullOrEmpty(dataNascimento.Text) || string.IsNullOrWhiteSpace(dataNascimento.Text))
            {
                dataNascimentoMensagem.IsVisible = true;
                return true;
            }

            if(dataNascimento.Text.Length == 10)
            {
                string[] datas = dataNascimento.Text.Split('/');

                if (Convert.ToInt32(datas[0]) > 31 || Convert.ToInt32(datas[1]) > 12 || Convert.ToInt32(datas[2]) < DateTime.Now.AddYears(-150).Year)
                {
                    dataNascimentoMensagem.IsVisible = true;
                    return true;
                }
            }
            else
            {
                dataNascimentoMensagem.IsVisible = true;
                return true;

            }

            if(Convert.ToDateTime(dataNascimento.Text) > DateTime.Now.AddYears(-18))
            {
                dataNascimentoMensagem.Text = "* Necessario ser maior que 18 anos";
                dataNascimentoMensagem.IsVisible = true;
                return true;
            }

            dataNascimentoMensagem.IsVisible = false;

            return false;
        }

        public void ValidaTelefone(object sender, EventArgs args)
        {
            ValidaTelefone();
        }

        private bool ValidaTelefone()
        {
            if (string.IsNullOrEmpty(telefone.Text) || string.IsNullOrWhiteSpace(telefone.Text) || telefone.Text.Length < 11)
            {
                telefoneMensagem.IsVisible = true;
                return true;
            }


            telefoneMensagem.IsVisible = false;

            return false;
        }
        public void ValidaRg(object sender, EventArgs args)
        {
            ValidaRg();
        }

        private bool ValidaRg()
        {
            if (string.IsNullOrEmpty(rg.Text) || string.IsNullOrWhiteSpace(rg.Text) || rg.Text.Length < 11)
            {
                rgMensagem.IsVisible = true;
                return true;
            }               

            rgMensagem.IsVisible = false;

            return false;
        }

        public void ValidaCpf(object sender, EventArgs args)
        {
            ValidaCpf();
        }

        private bool ValidaCpf()
        {
            if (string.IsNullOrEmpty( cpf.Text) || (!string.IsNullOrEmpty(cpf.Text) && !Regex.IsMatch(cpf.Text, "[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}")))
            {
                cpfMensagem.IsVisible = true;
                return true;
            }

            cpfMensagem.IsVisible = false;

            return false;
        }

        public void ValidaEstadoCivil(object sender, EventArgs args)
        {
            ValidaEstadoCivil();
        }

        private bool ValidaEstadoCivil()
        {
            if (estadocivil.SelectedIndex == -1)
            {
                estadocivilMensagem.IsVisible = true;
                return true;
            }

            estadocivilMensagem.IsVisible = false;

            return false;
        }

        public void ValidaGenero(object sender, EventArgs args)
        {
            ValidaGenero();
        }

        private bool ValidaGenero()
        {
            if (genero.SelectedIndex == -1 )
            {
                generoMensagem.IsVisible = true;
                return true;
            }          

            generoMensagem.IsVisible = false;

            return false;
        }

        public void ValidaEmail(object sender, EventArgs args)
        {
            ValidaEmail();
        }

        private bool ValidaEmail()
        {
            string emailValidacao = email.Text;

            if (string.IsNullOrEmpty(emailValidacao) || string.IsNullOrWhiteSpace(emailValidacao) )
            {               
                emailMensagem.IsVisible = true;
                return true;
            }               
            else
                emailMensagem.IsVisible = false;

            return false;
        }

        public void ValidaSenha(object sender, EventArgs args)
        {
            ValidaSenha();
        }

        private bool ValidaSenha()
        {
            string requisitosSenha = "^(?=.*[A-Z])(?=.*[a_z])(?=.*[!#@$%&.])(?=.*[0-9]).{8,}$";

            if (string.IsNullOrEmpty(senha.Text) || (!string.IsNullOrEmpty(senha.Text) && !Regex.IsMatch(senha.Text, requisitosSenha)))
            {
                senhaMensagem.Text = "* SENHA DEVE CONTER NO MÍNIMO 8 CARACTERES COM LETRAS MAIÚSCULAS E MINÚSCULA, NÚMEROS E UM CARÁTER ESPECIAL ";
                senhaMensagem.IsVisible = true;
                return true;
            }
            else
                senhaMensagem.IsVisible = false;

            return false;
        }

        public void ValidaContraSenha(object sender, EventArgs args)
        {
            ValidaContraSenha();
        }

        private bool ValidaContraSenha()
        {
            if (contrasenha.Text == null || contrasenha.Text.Length == 0 || contrasenha.Text != senha.Text)
            {
                contrasenhaMensagem.Text = "* SENHAS NÃO CONFERE";
                contrasenhaMensagem.IsVisible = true;
                return true;
            }
            else
                contrasenhaMensagem.IsVisible = false;

            return false;
        }

        #endregion
        /// <summary>
        /// Executa todos os metodos de validação de campos e returna false se algum estiver errado
        /// </summary>
        /// <returns></returns>
        private bool ExisteCampoSemErro()
        {
            if (ValidaNome())
                return false;

            if (ValidaSobrenome())
                return false;

            if (ValidaDtNascimento())
                return false;

            if (ValidaTelefone())
                return false;

            if (ValidaRg())
                return false;

            if (ValidaCpf())
                return false;

            if (ValidaEstadoCivil())
                return false;

            if (ValidaGenero())
                return false;

            if (ValidaEmail())
                return false;

            if (ValidaSenha())
                return false;

            if (ValidaContraSenha())
                return false;

            return true;
        }

        private void btnMenuCustom_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(EventArgs.Empty, "OpenMenu");
        }
    }
}