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

    public partial class ProductoVM : ObservableObject
    {
        private readonly TrackingDbContext _context;
        private ComprasProductosDetalleDTO _compradetalleDTO;
        private readonly IAPIManager _apiManager;
        public ProductoVM(ComprasProductosDetalleDTO compradetalleDTO)
        {
            _compradetalleDTO = compradetalleDTO;

            _apiManager = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IAPIManager>();

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
        private string lotReferencia;

        [ObservableProperty]
        private int comSecuencia;

        // TODO Sacar el origen de usos multiples
        [ObservableProperty]
        private int recDestino;

        [ObservableProperty]
        private string destino;

        [ObservableProperty]
        private string nombreProducto;

        [ObservableProperty]
        private decimal? comPeso;

        [ObservableProperty]
        private decimal? recPeso;
        #endregion

        public async void Inicio(int idProducto)
        {

            RepCodigo = _compradetalleDTO.RepCodigo;
            ComSecuencia = _compradetalleDTO.ComSecuencia;
            ComPeso = _compradetalleDTO.ComCantidad;
            IdProducto = _compradetalleDTO.ProId.Value;
            NombreProducto = _compradetalleDTO.ProDescripcion;
            //ComReferencia = _compradetalleDTO.ComReferencia;

        }

        #region Commands
        [RelayCommand]
        private async Task AddLote()
        {
            string resultado = await Shell.Current.DisplayPromptAsync("Nuevo Lote", "Escane el Lote", accept: "Aceptar", cancel: "Cancelar");

            try
            {

                if (!string.IsNullOrEmpty(resultado))
                {
                    LoadingEsVisible = true;
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
                        var lote = new LotesSecadoNaturalDTO();
                        LoadingEsVisible = false;
                    }
                }
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Guardar", "Ups, Algo no salio como esperaba\n" + e.Message, "OK");
            }
            finally
            {
                LoadingEsVisible = false;
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

            try
            {
                //await Task.Run(async () =>
                //{
                LoadingEsVisible = true;

                RecepcionesComprasDetalleDTO recepcionDetalle = new()
                {
                    ComPeso = ComPeso,
                    RecFechaRecogida = DateTime.Now,
                    RecPeso = RecPeso,
                    AlmCodigo = Destino == "Fermentación" ? "1" : "2",
                    RefSecuencia = ComSecuencia,
                    RepCodigo = RepCodigo,
                    ComReferencia = _compradetalleDTO.ComReferencia,
                    RecReferencia = ComReferencia,
                    NombreProducto = NombreProducto,
                    RecEstado = 1,
                };

                //recepcionDetalle.recepcionesComprasDTO = new RecepcionesCompraDTO()
                //{
                //    RepCodigo = RepCodigo,
                //    RecFecha = DateTime.Now,
                //    AlmCodigo = Destino == "Fermentación" ? "1" : "2",
                //    RecImpresa = false,
                //    //RecEstado = 1,
                //    OrdId = 1,
                //    ComSecuencia = ComSecuencia,
                //    RecFechaCreacion = DateTime.Now,
                //    Comreferencia = ComReferencia
                //};


                var Prod = await _apiManager.GuradarDetalleRecepcion(recepcionDetalle);

                MainThread.BeginInvokeOnMainThread(async () =>
               {
                   LoadingEsVisible = false;
                   WeakReferenceMessenger.Default.Send(new RecepcionDetalleMessage(Prod));
                   await Shell.Current.Navigation.PopAsync();
               });
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Guardar", "Ups, Algo no salio como esperaba\n" + e.Message, "OK");
            }
            finally
            {
                LoadingEsVisible = false;
            }

        }

        #endregion
    }
}
