using Tracking.DataAccess;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Tracking.Pages;
using System.Collections.ObjectModel;
using Tracking.Services;

namespace Tracking.ViewModels
{
    public partial class MainVM : ObservableObject
    {
        public class menuOptions
        {
            public string Name { get; set; }
            public Color Color { get; set; }
            public string Image { get; set; }
            public string NavigatePage { get; set; }
        }

        private readonly TrackingDbContext _context;
        private readonly IAPIManager _apiManager;
        public MainVM(TrackingDbContext context, IAPIManager apiManager)
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                 {
                     if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                     {
                         await Shell.Current.DisplayAlert("Internet", "Upps, Not internet access, Verify plase!", "OK");
                     }
                 });

                _apiManager = apiManager;

                _context = context;
                Options = new ObservableCollection<menuOptions>() {
                new menuOptions {Name = "Recepcion Compras", Color= Colors.Red, Image="recepcion.png"},
                new menuOptions {Name = "Cierre Lotes", Color=Colors.Green, Image="cierrepreview.png"},
                new menuOptions {Name = "Lotes\nFermentacion", Color=Colors.Blue, Image="fermentacionpreview.png"},
                new menuOptions {Name = "Lotes Secado Maquina", Color=Colors.Purple, Image="maquinapreview.png"},
                new menuOptions {Name = "Lotes Secado Natrual", Color=Colors.Brown, Image="naturalpreview.png"},
                new menuOptions {Name = "Lotes Limpiezas", Color=Colors.Black, Image="limpiezapreview.png"}
            };
            }
            catch (Exception e)
            {
                Shell.Current.Navigation.PushModalAsync(new SettingDataPage(new SettingDataMV(new TrackingDbContext())));
            }

        }

        [ObservableProperty]
        private bool loadingEsVisible = false;

        [ObservableProperty]
        private decimal totalIngresos;

        [ObservableProperty]
        private int totalVentas;

        [ObservableProperty]
        private int totalProductos;

        [ObservableProperty]
        private int totalCategorias;

        [ObservableProperty]
        public ObservableCollection<menuOptions> options;

        [ObservableProperty]
        public menuOptions selectedOptions;

        [RelayCommand]
        public async Task SelectionChanged()
        {
            LoadingEsVisible = true;

            try
            {

                switch (SelectedOptions.Name)
                {
                    case "Recepcion Compras":
                        await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionListMV(new DataAccess.TrackingDbContext(), _apiManager)));
                        break;
                    case "Cierre Lotes":
                        //await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.TrackingDbContext())));
                        break;
                    case "Lotes Fermentacion":
                        //await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.TrackingDbContext())));
                        break;
                    case "Lotes Secado Maquina":
                        //await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.TrackingDbContext())));
                        break;
                    case "Lotes Limpiezas":
                        //await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.TrackingDbContext())));
                        break;
                    default:
                        break;
                }

                await Task.Run(async () =>
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        SelectedOptions = null;
                        LoadingEsVisible = false;
                    });
                });

            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("SelectionChanged", "Ups, Algo no salio como esperaba\n" + e.Message, "OK");
            }

        }
    }
}
