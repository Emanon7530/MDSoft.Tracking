using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MDSoft.Tracking.Services.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tracking.Pages;

namespace Tracking.ViewModels
{
    public partial class TestPageViewModel : ObservableObject
    {
        public class menuOptions
        {
            public string Name { get; set; }
            public Color Color { get; set; }
            public string Image { get; set; }
            public string NavigatePage { get; set; }
        }

        [ObservableProperty]
        public ObservableCollection<menuOptions> options;

        [ObservableProperty]
        public menuOptions selectedOptions;

        [ObservableProperty]
        public bool loadingEsVisible;

        public TestPageViewModel()
        {

            Options = new ObservableCollection<menuOptions>() {
                new menuOptions {Name = "Recepcion Compras", Color= Colors.Red, Image="recepcion.png"},
                new menuOptions {Name = "Cierre Lotes", Color=Colors.Green, Image="cierrepreview.png"},
                new menuOptions {Name = "Lotes Fermentacion", Color=Colors.Blue, Image="fermentacionpreview.png"},
                new menuOptions {Name = "Lotes Secado Maquina", Color=Colors.Purple, Image="maquinapreview.png"},
                new menuOptions {Name = "Lotes Secado Natrual", Color=Colors.Brown, Image="naturalpreview.png"},
                new menuOptions {Name = "Lotes Limpiezas", Color=Colors.Black, Image="limpiezapreview.png"},
            };

            LoadingEsVisible = false;

            Task.Run(async () =>
            {
                await LoadAPI();

            });


        }

        public async Task LoadAPI()
        {

            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://movilbusiness.com.do/MDSOFT_Tracking/")
            };

            string endPoint = $"ComprasProductos/sp_GetComprasPendientes";

            var response = await client.GetAsync(endPoint);

            if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("no existe!", response.RequestMessage.ToString(), "OK");
            }
            else
            {

                var responsedetail = await response.Content.ReadAsStringAsync();
                var compras = JsonConvert.DeserializeObject<List<ComprasProductoDTO>>(responsedetail);
            }

        }

        [RelayCommand]
        public async Task SelectionChanged()
        {
            LoadingEsVisible = true;

            //switch (SelectedOptions.Name)
            //{
            //    case "Recepcion Compras":
            //        await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.TrackingDbContext())));
            //        break;
            //    case "Cierre Lotes":
            //        await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.TrackingDbContext())));
            //        break;
            //    case "Lotes Fermentacion":
            //        await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.TrackingDbContext())));
            //        break;
            //    case "Lotes Secado Maquina":
            //        await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.TrackingDbContext())));
            //        break;
            //    case "Lotes Limpiezas":
            //        await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.TrackingDbContext())));
            //        break;
            //    default:
            //        break;
            //}

            await Task.Run(async () =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    LoadingEsVisible = false;
                });
            });


        }



    }
}
