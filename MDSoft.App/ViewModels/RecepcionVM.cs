using CommunityToolkit.Mvvm.ComponentModel;
using Tracking.Utilidades;
using CommunityToolkit.Mvvm.Input;
using Tracking.Pages;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using Tracking.DataAccess;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Tracking.Modelos;
using MDSoft.Tracking.Services.DTO;
using Tracking.Services;
using MDSoft.Tracking.Services;
using Microsoft.Identity.Client;
using PNComm.Common.Enums;

namespace Tracking.ViewModels
{
    public partial class RecepcionVM : ObservableObject
    {
        private readonly TrackingDbContext _context;
        private readonly IAPIManager _apiManager;
        private ComprasProductoDTO _compraDTO;
        public RecepcionVM(ComprasProductoDTO compraDTO)
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
                _apiManager = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IAPIManager>();

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
        private ObservableCollection<RecepcionesComprasDetalleDTO> detalleRecepcion = new ObservableCollection<RecepcionesComprasDetalleDTO>();

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
                    await Shell.Current.Navigation.PushModalAsync(new ProductoPage(new ProductoVM(_compradetalleDTO), 0));
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
            await Shell.Current.Navigation.PushModalAsync(new PesoLinealPage(new PesoLinealVM(new ComprasProductoDTO()), 0));
            await Task.Run(async () =>
            {
                //await GetRecepciones();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    BuscarRecepcion = "";
                    LoadingEsVisible = false;
                    MostarTotal();
                });
            });

        }

        [RelayCommand]
        private async Task Limpiar()
        {
            LoadingEsVisible = true;

            BuscarRecepcion = "";
            //await GetRecepciones();

            LoadingEsVisible = false;

        }

        [RelayCommand]
        private async void Eliminar(RecepcionesComprasDetalleDTO detalle)
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

                RecepcionesCompraDTO recepcion = new RecepcionesCompraDTO()
                {
                    RecFechaActualizacion = DateTime.Now,
                    RepCodigo = _compraDTO.RepCodigo,
                    RecFecha = DateTime.Now,
                    RecReferencia = _compraDTO.ComReferencia,
                    RecEstado = 2, // abierta
                    RecFechaCreacion = DateTime.Now,
                    ComSecuencia = ComSecuencia,
                    RecSecuencia = RecSecuencia,
                    RecepcionesComprasDetallesDTO = DetalleRecepcion
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

        //public async Task GetRecepciones()
        //{
        //    LoadingEsVisible = true;

        //    try
        //    {
        //        var compras = await _apiManager.GetRecepcionesDetalle(RecSecuencia);

        //        var lstTemp = new ObservableCollection<RecepcionesComprasDetalleDTO>();

        //        if (compras.Any())
        //        {
        //            foreach (var item in compras)
        //            {
        //                lstTemp.Add(new RecepcionesComprasDetalleDTO
        //                {
        //                    ComReferencia = item.ComReferencia,
        //                    RepCodigo = item.RepCodigo,
        //                    ComPeso = item.ComPeso,
        //                    RecEstado = item.RecEstado,
        //                    //RecFechaCreacion = item.RecFechaCreacion,
        //                    RecPeso = item.RecPeso,
        //                    RecPosicion = item.RecPosicion,
        //                    recepcionesComprasDTO = item.recepcionesComprasDTO
        //                });
        //            }

        //        }

        //        DetalleRecepcion = lstTemp;

        //    }
        //    catch (Exception e)
        //    {
        //        await Shell.Current.DisplayAlert("Error!", $"No se pudo recuperar las compras \\n {e.Message} ", "Aceptar");
        //    }
        //    finally
        //    {
        //        LoadingEsVisible = false;
        //    }

        //}

        private void CompraMensajeRecibido(CompraResult result)
        {
            RepCodigo = result.Compra.RepCodigo;
            ComSecuencia = result.Compra.ComSecuencia;
            ProNombre = result.Compra.RepNombre;
            TotalpesoCompra = result.Compra.ComCantidadDetalle.Value;

        }

        private void RecepcionDetalleMensajeRecibido(RecepcionesComprasDetalleDTO result)
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
