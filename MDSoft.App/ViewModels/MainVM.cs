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
        private readonly VentaDbContext _context;
        public MainVM(VentaDbContext context)
        {
            _context = context;
            Options = new ObservableCollection<menuOptions>() {
                new menuOptions {Name = "Recepcion Compras", Color= Colors.Red, Image="recepcion.png"},
                new menuOptions {Name = "Cierre Lotes", Color=Colors.Green, Image="cierrepreview.png"},
                new menuOptions {Name = "Lotes Fermentacion", Color=Colors.Blue, Image="fermentacionpreview.png"},
                new menuOptions {Name = "Lotes Secado Maquina", Color=Colors.Purple, Image="maquinapreview.png"},
                new menuOptions {Name = "Lotes Secado Natrual", Color=Colors.Brown, Image="naturalpreview.png"},
                new menuOptions {Name = "Lotes Limpiezas", Color=Colors.Black, Image="limpiezapreview.png"},
            };

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(async () => await ObtenerResumen());
            });
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

        private async Task ObtenerResumen()
        {
            //decimal totalingresos = 0;
            //var lstVentas = await _context.Ventas.ToListAsync();
            //foreach (var item in lstVentas)
            //{
            //    totalingresos += item.Total;
            //}

            //TotalIngresos = totalingresos;
            //TotalVentas = _context.Ventas.Count();
            //TotalProductos = _context.Productos.Count();
            //TotalCategorias = _context.Categorias.Count();
        }
        [RelayCommand]
        public async Task SelectionChanged()
        {
            LoadingEsVisible = true;

            switch (SelectedOptions.Name)
            {
                case "Recepcion Compras":
                    await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.VentaDbContext())));
                    break;
                case "Cierre Lotes":
                    await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.VentaDbContext())));
                    break;
                case "Lotes Fermentacion":
                    await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.VentaDbContext())));
                    break;
                case "Lotes Secado Maquina":
                    await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.VentaDbContext())));
                    break;
                case "Lotes Limpiezas":
                    await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.VentaDbContext())));
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
        [RelayCommand]
        private async Task Recepcion()
        {
            LoadingEsVisible = true;

            await Shell.Current.Navigation.PushAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.VentaDbContext())));

            await Task.Run(async () =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    LoadingEsVisible = false;
                });
            });

        }

        [RelayCommand]
        private async Task SecadoMaquina()
        {
            LoadingEsVisible = true;

            await Task.Run(async () =>
            {
                //await Shell.Current.Navigation.PushAsync(new CategoriasPage(new CategoriasVM(new DataAccess.VentaDbContext())));

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    LoadingEsVisible = false;
                });
            });

        }

    }
}
