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
	public partial class NaoConfirmadaPage : ContentPage
	{
		public NaoConfirmadaPage ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs args)
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}