using Tracking.DataAccess;
using Tracking.Modelos;
using Tracking.Pages;
using Tracking.Utilidades;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Formats.Tar;
using System.IO;
using System.Reflection;
using MDSoft.Tracking.Services.DTO;
using Tracking.Services;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Input;

namespace Tracking.ViewModels
{

    public partial class PesoLinealVM : ObservableObject
    {
        private readonly TrackingDbContext _context;
        private ComprasProductoDTO _compradetalleDTO;
        private readonly IAPIManager _apiManager;
        public PesoLinealVM(ComprasProductoDTO compradetalleDTO)
        {
            _compradetalleDTO = compradetalleDTO;

            _apiManager = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IAPIManager>();

            //MainThread.BeginInvokeOnMainThread(async () =>
            //    {
            //        await Task.Run(async () => Inicio(0));
            //        //await Task.Run(async () => await ShowInfoProduct());
            //    });

        }

        #region Propiedades
        private int IdProducto;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        [ObservableProperty]
        private string repCodigo = string.Empty;

        [ObservableProperty]
        private string comReferencia = string.Empty;

        [ObservableProperty]
        private string lotReferencia ;

        [ObservableProperty]
        private int comSecuencia;

        [ObservableProperty]
        private int recDestino;

        [ObservableProperty]
        private string destino;

        [ObservableProperty]
        private decimal? comPeso;

        [ObservableProperty]
        private int recPeso;

        [ObservableProperty]
        private int recTipoproducto;
        #endregion

        public async void Inicio(int idProducto)
        {
            RepCodigo = _compradetalleDTO.RepCodigo;
            ComSecuencia = _compradetalleDTO.ComSecuencia;
        }

        #region Commands
        [RelayCommand]
        private async Task AddLote()
        {
            string resultado = await Shell.Current.DisplayPromptAsync("Nuevo Lote", "Escane el Lote", accept: "Aceptar", cancel: "Cancelar");

            if (!string.IsNullOrEmpty(resultado))
            {
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
                    if (Destino == "Fermentación") // Fermentacion
                    {
                        var lote = new LotesFermentacionDTO()
                        {
                            LotFermentacion = resultado,
                            LotFechaCreacion = DateTime.Now,
                            LotFechaCierre = null,
                            LotesFermentacionDetallesDTO = null

                        };
                        var compras = await _apiManager.GuardarLoteFermentacion(lote);
                    }
                    else // Secado a Maquina
                    {
                        var lote = new LotesSecadoNaturalDTO()
                        {
                            LotSecadoManual = resultado,
                            LotFechaCreacion = DateTime.Now,
                            LotFechaCierre = null,
                            LotesSecadoNaturalDetallesDTO = null
                        };
                        var compras = await _apiManager.GuardarLoteSecadoNatural(lote);
                    }

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        ComReferencia = resultado;
                        LoadingEsVisible = false;
                    });
                });
            }
        }

        [RelayCommand]
        private async Task VolverInventario()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        [RelayCommand]
        private async Task Guardar()
        {

            await Task.Run(async () =>
            {
                LoadingEsVisible = true;

                RecepcionesComprasDetalleDTO recepcionDetalle = new()
                {
                    ComPeso = ComPeso,
                    RecFechaCreacion = DateTime.Now,
                    RecPeso = RecPeso,
                    RecDestino = Destino == "Fermentacion" ? 1 : 2,
                    //RecPosicion = _compradetalleDTO.ComPosicion,
                    RepCodigo = RepCodigo,
                    ComReferencia = _compradetalleDTO.ComReferencia,
                    LotReferencia = LotReferencia,
                    RecEstado = "Rec",
                    RecSecuencia = 1
                };

                recepcionDetalle.recepcionesComprasDTO = new RecepcionesCompraDTO()
                {

                    RecSecuencia = 1,
                    RecEstado = "Prac",
                    RecFechaCreacion = DateTime.Now,
                    ComReferencia = _compradetalleDTO.ComReferencia
                };


                var Prod = await _apiManager.GuradarDetalleRecepcion(recepcionDetalle);

                MainThread.BeginInvokeOnMainThread(async () =>
               {
                   LoadingEsVisible = false;
                   WeakReferenceMessenger.Default.Send(new RecepcionDetalleMessage(recepcionDetalle));
                   await Shell.Current.Navigation.PopAsync();
               });
            });

        }

        #endregion
    }
}
