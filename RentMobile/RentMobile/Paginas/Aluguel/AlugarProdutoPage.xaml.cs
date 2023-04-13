using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlugarProdutoPage : ContentPage
	{
		public AlugarProdutoPage ()
		{
			InitializeComponent ();

            try
            {
                var byteArray = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAwAAAAMCAYAAABWdVznAAAAw0lEQVQokZXRsW3DQAyF4Z8U1KjSCFZ1UBdNEHsTbeCM4oyQSaIN5I64ShrB1cFQcUxhOVXgSK8kvgcQpLBmHMe6qqoPEXl39+M6vgKXEMLX0wmAmR1U9Rs48HfmnPOpbdtZAGKM0wv8W0opdYWZ9SLS/4MB6rIs71oUxXkDfuwvcpQYo28tAOgeDKAiMuzwV3X3PYWLTNNUL8sysuGsIYRGm6a55ZxPwPwKr+bx6WfMrFfVM/AG4O6DiAwppc+u624APwkARp6PfCCZAAAAAElFTkSuQmCC");

                var byteArray2 = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAA4AAAAOCAYAAAAfSC3RAAABEElEQVQ4jZ3SsW0CUQzG8b99EXVGuBGIEHXIBtkA6HwnJG4DSJeW5kEXsgGZAKWjQIIRGCHpoof0nIaTAImA8nW2/LMbwz8j542qqu5jjBXQBXIAEdm6+zal9DKbzXYAeozMrBdj3ADu7r0QgoQQJMbYFxFUdVMURXVysSiKSkSGKaWneut5zCxX1aW7j+WosUkpPVxC51gBsiwbAYtrCOAwM7871M39ft+/huqklD4BKMvSb0V19PrI33BnZp1bUVmWzRrOVfXxVigiQwVoNBoToGdm+TVkZrm7P2cAq9Xqp91uf4vIW6vV+liv11+XkKougdeTXz18zwhYAJMQwhZgMBh0UkodoOvu4+l0+v4LKz11XHIDpBkAAAAASUVORK5CYII=");

                var imageSource = ImageSource.FromStream(() => new MemoryStream(byteArray));
                Elipse1.Source = imageSource;

                var imageSource2 = ImageSource.FromStream(() => new MemoryStream(byteArray2));
                Elipse2.Source = imageSource2;
                Elipse3.Source = imageSource2;
                Elipse4.Source = imageSource2;
                Elipse5.Source = imageSource2;
            }
            catch(Exception ex)
            {
                string log = ex.Message;
            }
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            
        }
    }
}