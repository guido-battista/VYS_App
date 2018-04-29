using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYS_App.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaCanciones : TabbedPage
    {
        ObservableCollection<ApiRest.CancionResult> cancionesAVotar = new ObservableCollection<ApiRest.CancionResult>();
        ObservableCollection<ApiRest.CancionResult> cancionesYaEscuchadas = new ObservableCollection<ApiRest.CancionResult>();
        ApiRest.CancionResult cancionAVotar;

        const String url = "https://vys-server.herokuapp.com";
        //const String url = "http://192.168.1.6:3000";

        public ListaCanciones(String idEvento, String codigoEvento)
        {
            InitializeComponent();
            ListaVotar.ItemsSource = cancionesAVotar;
            ListaYaEscuchadas.ItemsSource = cancionesYaEscuchadas;
            Device.BeginInvokeOnMainThread(cargarCanciones);
            //cargarCanciones();
        }

        private async void cargarCanciones()
        {
            ProgresoVotar.IsVisible = true;
            ProgresoVotar.IsRunning = true;
            ProgresoYaEscuchadas.IsVisible = true;
            ProgresoYaEscuchadas.IsRunning = true;

            //Se limpia a manopla la listae
            while (cancionesAVotar.Count() > 0)
            {
                this.cancionesAVotar.RemoveAt(0);
            }

            while (cancionesYaEscuchadas.Count() > 0)
            {
                this.cancionesYaEscuchadas.RemoveAt(0);
            }

            ApiRest.RestClient client = new ApiRest.RestClient();
            var resultadoCanciones = await client.Get<ApiRest.CancionResult[]>(url + "/canciones");

            if (resultadoCanciones != null)
            {
                foreach (ApiRest.CancionResult cancion in resultadoCanciones)
                {
                    if (cancion.estado == "Votar")
                        cancionesAVotar.Add(cancion);
                    if (cancion.estado == "Escuchada")
                        cancionesYaEscuchadas.Add(cancion);

                }
            }

            ProgresoVotar.IsVisible = false;
            ProgresoVotar.IsRunning = false;
            ProgresoYaEscuchadas.IsVisible = false;
            ProgresoYaEscuchadas.IsRunning = false;
        }

        async void OnTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            ApiRest.CancionResult cancionMarcada = (ApiRest.CancionResult)e.Item;
            var answer = await DisplayAlert("¿Quieres votar este tema?", cancionMarcada.titulo, "Yes", "No");
            //Si se pone YES, toma el valor TRUE
            if (answer)
            {
                cancionAVotar = cancionMarcada;
                Device.BeginInvokeOnMainThread(votarCancion);

                Device.BeginInvokeOnMainThread(cargarCanciones);
            }
        }

        async void SonandoClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Cancion Sonando", "Propuesta Indecente - Romeo Santos", "OK");
        }

        void CambiarEventoClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Vista.ElegirEvento();
        }

        void RefreshClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(cargarCanciones);
        }

        private async void votarCancion()
        {
            ApiRest.RestClient client = new ApiRest.RestClient();
            var resultadoCanciones = await client.Post<ApiRest.CancionResult>(url + "/sumarVoto", cancionAVotar);
            return;

        }
    }
}