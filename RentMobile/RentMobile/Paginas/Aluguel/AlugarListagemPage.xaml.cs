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
	public partial class AlugarListagemPage : ContentPage
	{
		public AlugarListagemPage ()
		{
			InitializeComponent ();
		}

        private void btnMenuCustom_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(EventArgs.Empty, "OpenMenu");
        }

        private void Outros_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AlugarProdutoPage());
        }
    }
}