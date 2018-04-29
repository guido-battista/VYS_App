using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYS_App.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ElegirEvento : ContentPage
    {
        public ElegirEvento()
        {
            InitializeComponent();
            //NavigationController.SetNavigationBarHidden(true, true);
        }

        async void IngresarClicked(object sender, EventArgs e)
        {
            /*
            if (IdEvento.Text.Equals("1") && CodigoEvento.Text.Equals("12"))
            {
                App.Current.MainPage = new NavigationPage(new Vista.ListaCanciones(IdEvento.Text, CodigoEvento.Text))
                {
                    BarBackgroundColor = Color.DarkGreen,
                };
                //await this.Navigation.PushAsync(new Vista.ListaCanciones(IdEvento.Text, CodigoEvento.Text));
            }
            else
            {
                await DisplayAlert("Ojota!!!", "Datos ingresados incorrectos", "Volver");
            }
            */
            App.Current.MainPage = new NavigationPage(new Vista.ListaCanciones(IdEvento.Text, CodigoEvento.Text))
            {
                BarBackgroundColor = Color.DarkGreen,
            };

        }
    }
}