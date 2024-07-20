using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MDSoft.Tracking.Services.Dto;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Enums;
using PNComm.Common.Enums;
using System.Collections.ObjectModel;
using Tracking.DataAccess;
using Tracking.Services;
using Tracking.Utilidades;

namespace Tracking.ViewModels
{

    public partial class PesoLinealVM : ObservableObject
    {
        private readonly TrackingDbContext _context;
        private ComprasProductoDTO _compraproductoDTO;
        private readonly IAPIManager _apiManager;
        public PesoLinealVM(ComprasProductoDTO compraproductoDTO, IAPIManager apiManager)
        {
            _compraproductoDTO = compraproductoDTO;
            RepCodigo = compraproductoDTO.RepCodigo;
            ComSecuencia = compraproductoDTO.ComSecuencia;

            _apiManager = apiManager;


            MainThread.BeginInvokeOnMainThread(async () =>
            {
                RecListaTipos = new ObservableCollection<TipoProductoDTO>(await _apiManager.GetAllTipoProducto());
            });
        }

        #region Propiedades
        private int IdProducto;
        private bool comReferenceExists = false;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        [ObservableProperty]
        private string repCodigo = string.Empty;

        [ObservableProperty]
        private string comReferencia = string.Empty;

        [ObservableProperty]
        private string lotReferencia;

        [ObservableProperty]
        private TipoProductoDTO recTipoproducto;

        [ObservableProperty]
        private ObservableCollection<TipoProductoDTO> recListaTipos;

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

            RepCodigo = _compraproductoDTO.RepCodigo;
            ComSecuencia = _compraproductoDTO.ComSecuencia;
            ComPeso = _compraproductoDTO.ComCantidadDetalle;
            //IdProducto = _compraproductoDTO.ProId.Value;
            //NombreProducto = _compraproductoDTO.ProDescripcion;

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
                            LotUsuarioCreacion = Preferences.Get("usuario", ""),
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
                            UsuiniciosesionCreacion = Preferences.Get("usuario", ""),
                            LotesSecadoNaturalDetallesDTO = null

                        };
                        var compras = await _apiManager.GuardarLoteSecadoNatural(lote);
                    }
                    ComReferencia = resultado;
                    comReferenceExists = true;
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
        private async Task SelectedIndexChanged()
        {
            var Prod = await _apiManager.GetProductInCompraByCode(RepCodigo, ComSecuencia, RecTipoproducto.ProId);
            ComPeso = Prod.Sum(x => x.ComPesoKg);
        }

        [RelayCommand]
        private async Task Guardar()
        {

            try
            {

                if (string.IsNullOrEmpty(ComReferencia))
                {
                    await Shell.Current.DisplayAlert("Informacion", "Numero de lote requerido!", "OK");
                    return;
                }

                if (RecPeso <= 0)
                {
                    await Shell.Current.DisplayAlert("Informacion", "Peso de Recepción es requerido!", "OK");
                    return;
                }

                if (!comReferenceExists)
                {
                    if (Destino == "Fermentación")
                    {
                        comReferenceExists = await _apiManager.LoteFermentacionExists(ComReferencia);
                    }
                    else
                    {
                        comReferenceExists = await _apiManager.LoteSecadoNaturalExists(ComReferencia);
                    }

                    if (!comReferenceExists)
                    {
                        await Shell.Current.DisplayAlert("Informacion", "Numero de Lote no existe, Verifique!", "OK");
                        return;
                    }
                }

                if (ComPeso != RecPeso)
                {
                    var answer = await Shell.Current.DisplayAlert("Recepcion", "Peso recibido es diferente a peso Comprado, continuar?", "Guardar", "Corregir");

                    if (!answer)
                    {
                        return;
                    }
                }

                LoadingEsVisible = true;

                RecepcionesProductosDetalleDTO recepcionDetalle = new()
                {
                    ComPeso = ComPeso,
                    RecPeso = RecPeso,
                    RecDestino = Destino == "Fermentación" ? ((int)LoteDestinoEnum.LoteFermentacion).ToString() : ((int)LoteDestinoEnum.LoteSecadoNatural).ToString(),
                    RecSecuencia = ComSecuencia,
                    ComSecuencia = ComSecuencia,
                    ProId = RecTipoproducto.ProId,
                    RepCodigo = RepCodigo,
                    //ComReferencia = _compraproductoDTO.ComReferencia,
                    RecReferencia = ComReferencia,
                    //NombreProducto = NombreProducto,
                    UsuInicioSesion = Preferences.Get("usuario", ""),
                    UsuFechaActualizacion = DateTime.Now,
                    RecFecha = DateTime.Now,
                    RecEstado = (int)EstatusRecepcionProductos.Cerrada,
                };

                var Prod = await _apiManager.GuradarLinear(recepcionDetalle);

                MainThread.BeginInvokeOnMainThread(async () =>
               {
                   LoadingEsVisible = false;
                   WeakReferenceMessenger.Default.Send(new RecepcionDetalleLinearMessage(Prod));
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
