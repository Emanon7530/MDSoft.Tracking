using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MDSoft.Tracking.Services.DTO;
using PNComm.Common.Enums;
using System.Collections.ObjectModel;
using Tracking.DataAccess;
using Tracking.Pages;
using Tracking.Services;
using Tracking.Utilidades;

namespace Tracking.ViewModels
{
    public partial class RecepcionVM : ObservableObject
    {
        private readonly TrackingDbContext _context;
        private readonly IAPIManager _apiManager;
        private ComprasProductoDTO _compraDTO;
        public RecepcionVM(ComprasProductoDTO compraDTO, IAPIManager apiManager)
        {
            try
            {
                WeakReferenceMessenger.Default.Register<RecepcionDetalleMessage>(this, (r, m) =>
                {
                    RecepcionDetalleMensajeRecibido(m.Value);
                });

                WeakReferenceMessenger.Default.Register<CompraMessage>(this, (r, m) =>
                {
                    CompraMensajeRecibido(m.Value);
                });

                _compraDTO = compraDTO;
                _apiManager = apiManager;

                TotalpesoCompra = decimal.Parse(compraDTO.ComCantidadDetalle.ToString());

                PropertyChanged += RecepcionVM_PropertyChanged;
            }
            catch (Exception e)
            {
                Shell.Current.DisplayAlert("Error!", $"No se pudo recuperar las compras \\n {e.Message} ", "Aceptar");
            }
        }

        private void RecepcionVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BuscarRecepcion))
            {
                BtnLimpiarEsVisible = !(string.IsNullOrEmpty(BuscarRecepcion));
            }

            if (e.PropertyName == nameof(Totalpesorecibido))
            {
                if ((Totalpesorecibido - TotalpesoCompra) > 0)
                {
                    Diferencia = Totalpesorecibido - TotalpesoCompra;
                    CambioColor = System.Drawing.Color.Red;
                }
                else
                {
                    Diferencia = TotalpesoCompra - Totalpesorecibido;
                    CambioColor = System.Drawing.Color.Blue;
                }
            }
        }

        #region Propiedades publicas
        [ObservableProperty]
        private ObservableCollection<RecepcionesProductosDetalleDTO> detalleRecepcion = new ObservableCollection<RecepcionesProductosDetalleDTO>();

        [ObservableProperty]
        private string buscarRecepcion = string.Empty;

        [ObservableProperty]
        private string repCodigo;

        [ObservableProperty]
        private int recSecuencia;

        [ObservableProperty]
        private int comSecuencia;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        [ObservableProperty]
        private int idRepresentante;

        [ObservableProperty]
        private string nombreRepresentante;

        [ObservableProperty]
        private int idVendedor;

        [ObservableProperty]
        private string nombreProducto;

        [ObservableProperty]
        private string proNombre;

        [ObservableProperty]
        private int cantidadProductos;

        [ObservableProperty]
        private decimal totalpesorecibido;

        [ObservableProperty]
        private decimal totalpesoCompra;

        [ObservableProperty]
        private decimal diferencia;

        [ObservableProperty]
        private System.Drawing.Color cambioColor;

        [ObservableProperty]
        private bool btnLimpiarEsVisible = false;

        private Random rnd = new Random();

        #endregion

        #region Commands

        [RelayCommand]
        private async Task Buscar()
        {
            try
            {
                if (BuscarRecepcion == null)
                {
                    await Shell.Current.DisplayAlert("Buscar Producto", $"Escanee o digite el codigo del producto", "OK");
                    return;
                }

                LoadingEsVisible = true;

                var _compradetalleDTO = await _apiManager.GetProductInCompraByReference(RepCodigo, ComSecuencia, BuscarRecepcion);

                if (_compradetalleDTO == null)
                {
                    await Shell.Current.DisplayAlert("Buscar Producto", $"No existe producto con esta referencia {BuscarRecepcion}", "OK");
                }
                else
                {
                    await Shell.Current.Navigation.PushModalAsync(new ProductoPage(new ProductoVM(_compradetalleDTO, _apiManager), 0));
                }

            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error!", $"No se pudo recuperar las compras \\n {e.Message} ", "Aceptar");
            }
            finally
            {
                BuscarRecepcion = "";
                LoadingEsVisible = false;
                MostarTotal();

            }

        }

        [RelayCommand]
        private async Task PesoLineal()
        {

            LoadingEsVisible = true;

            var compras = new ComprasProductoDTO()
            {
                RepCodigo = RepCodigo,
                ComSecuencia = ComSecuencia
            };
            await Shell.Current.Navigation.PushModalAsync(new PesoLinealPage(new PesoLinealVM(compras, _apiManager), 0));

            MainThread.BeginInvokeOnMainThread(() =>
            {
                BuscarRecepcion = "";
                LoadingEsVisible = false;
                MostarTotal();
            });

        }

        //[RelayCommand]
        //private async Task Back()
        //{
        //    LoadingEsVisible = true;
        //    await Shell.Current.GoToAsync("..");
        //}


        [RelayCommand]
        private async Task Limpiar()
        {
            LoadingEsVisible = true;
            BtnLimpiarEsVisible = false;

            BuscarRecepcion = "";

            LoadingEsVisible = false;

        }

        [RelayCommand]
        private async void Eliminar(RecepcionesProductosDetalleDTO detalle)
        {
            try
            {
                LoadingEsVisible = true;

                //cierreParcial = await Shell.Current.DisplayAlert("Mensaje", "Desea cerra la recepcion con productos pendintes?", "Si", "No");
                DetalleRecepcion.Remove(detalle);
                MostarTotal();
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error!", $"No se pudo Eliminar la recepcion \\n {e.Message} ", "Aceptar");
            }
            finally
            {
                LoadingEsVisible = false;
            }
        }

        [RelayCommand]
        private async Task FinalizarVenta()
        {
            bool cierreParcial = false;
            if (DetalleRecepcion.Count < 1)
            {
                await Shell.Current.DisplayAlert("Mensaje", "No se han agregados productos a la recepcion", "Aceptar");
                return;
            }

            if (Totalpesorecibido == 0)
            {
                await Shell.Current.DisplayAlert("Mensaje", "Debe ingresar compras de productos", "Aceptar");
                return;
            }

            if (TotalpesoCompra > Totalpesorecibido)
            {
                cierreParcial = await Shell.Current.DisplayAlert("Mensaje", "Desea cerra la recepcion con productos pendintes?", "Si", "No");
            }

            try
            {
                LoadingEsVisible = true;

                _compraDTO.ComEstatus = cierreParcial == true ? (short?)EstatusCompraProductos.RecibidoParcial : (short?)EstatusCompraProductos.RecibidoTotal;

                RecepcionesProductoDTO recepcion = new RecepcionesProductoDTO()
                {
                    RecFechaActualizacion = DateTime.Now,
                    RepCodigo = _compraDTO.RepCodigo,
                    RecFecha = DateTime.Now,
                    RecReferencia = _compraDTO.ComReferencia,
                    RecEstado = (int)EstatusRecepcionProductos.Abierta, // abierta
                    UsuiniciosesionCreacion = Preferences.Get("usuario", ""),
                    ComEstadoCompra = cierreParcial == true ? (int) EstatusCompraProductos.RecibidoTotal : 
                                                                (int)EstatusCompraProductos.RecibidoTotal,
                    RecSecuencia = RecSecuencia,
                };

                await _apiManager.ActualizarRecepcion(recepcion);

                await Shell.Current.DisplayAlert("Listo!", string.Concat($"Recepcion {recepcion.RecReferencia} fue recibida ", cierreParcial == true ? "Parcial" : "Total"), "Aceptar");

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.Navigation.PopAsync();
                    var result = new RecepcionCompraResult();
                    result.recepcion = recepcion;

                    WeakReferenceMessenger.Default.Send(result);
                    LoadingEsVisible = false;
                });

                DetalleRecepcion.Clear();
                TotalpesoCompra = 0;
                Totalpesorecibido = 0;

            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error!", $"No se pudo registrar la recepcion \\n {e.Message} ", "Aceptar");
            }
            finally
            {
                LoadingEsVisible = false;
            }
        }

        [RelayCommand]
        private async Task Volver()
        {
            LoadingEsVisible = true;
            await Shell.Current.Navigation.PopAsync();
        }
        #endregion

        #region Functions
        public void MostarTotal()
        {
            TotalpesoCompra = DetalleRecepcion.Sum(c => c.ComPeso.Value);
            Totalpesorecibido = DetalleRecepcion.Sum(c => c.RecPeso.Value);
        }
        private void CompraMensajeRecibido(CompraResult result)
        {
            RepCodigo = result.Compra.RepCodigo;
            ComSecuencia = result.Compra.ComSecuencia;
            ProNombre = result.Compra.RepNombre;
            TotalpesoCompra = result.Compra.ComCantidadDetalle.Value;

        }

        private void RecepcionDetalleMensajeRecibido(RecepcionesProductosDetalleDTO result)
        {
            // TODO Asignar aqui el RecReferencia y el RecSecuencia
            RecSecuencia = result.RecSecuencia;
            DetalleRecepcion.Add(result);
            MostarTotal();
        }

        internal void Inicio(string idRecepcion)
        {

            RepCodigo = _compraDTO.RepCodigo;
            ComSecuencia = _compraDTO.ComSecuencia;

        }
        #endregion
    }
}
