using CommunityToolkit.Mvvm.ComponentModel;
using Tracking.Utilidades;
using CommunityToolkit.Mvvm.Input;
using Tracking.Pages;
using CommunityToolkit.Mvvm.Messaging;
using Tracking.DTOs;
using System.Collections.ObjectModel;
using Tracking.Modelos;
using Tracking.DataAccess;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace Tracking.ViewModels
{
    public partial class RecepcionVM : ObservableObject
    {
        private readonly VentaDbContext _context;
        public RecepcionVM(VentaDbContext context)
        {
            WeakReferenceMessenger.Default.Register<CompraDetalleMessage>(this, (r, m) =>
            {
                RecepcionDetalleMensajeRecibido(m.Value);
            });

            WeakReferenceMessenger.Default.Register<CompraMessage>(this, (r, m) =>
            {
                CompraMensajeRecibido(m.Value);
            });

            _context = context;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(async () => await GetRecepciones());
            });

            PropertyChanged += RecepcionVM_PropertyChanged;

        }

        private void RecepcionVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Totalpesorecibido))
            {
                if (Totalpesorecibido - TotalpesoCompra > 0)
                {
                    Diferencia = Totalpesorecibido - TotalpesoCompra;
                    CambioColor = System.Drawing.Color.Red;
                }
                else
                {
                    Diferencia = TotalpesoCompra - Totalpesorecibido;
                    CambioColor = System.Drawing.Color.Black;
                }
            }
        }

        [ObservableProperty]
        private ObservableCollection<RecepcionDetalleDTO> detalleRecepcion = new ObservableCollection<RecepcionDetalleDTO>();
        [ObservableProperty]
        private string buscarRecepcion = string.Empty;

        [ObservableProperty]
        private string codigoCompra;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        [ObservableProperty]
        private int idRepresentante;

        [ObservableProperty]
        private string nombreRepresentante;

        [ObservableProperty]
        private int secuencia;

        [ObservableProperty]
        private int idVendedor;

        [ObservableProperty]
        private string nombreProducto;

        [ObservableProperty]
        private string nombreVendedor;

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
        [RelayCommand]
        private async Task TapBuscar()
        {
            await Shell.Current.Navigation.PushModalAsync(new BuscarProductoPage(new BuscarProductoVM(new VentaDbContext())));
        }

        [RelayCommand]
        private async Task TapEscanear()
        {
            await Shell.Current.Navigation.PushModalAsync(new EscanearComprasPage(new VentaDbContext()));
        }

        [RelayCommand]
        private async Task TapEscanearProducto()
        {
            await Shell.Current.Navigation.PushModalAsync(new CompraProductoDetallePage(new CompraProductoDetalleMV(new VentaDbContext()), 0));
            //await Shell.Current.Navigation.PushModalAsync(new EscanearComrpaDetallePage(new VentaDbContext()));
        }

        private void CompraDetalleMensajeRecibido(CompraDetalleDTO result)
        {
            var encontrado = DetalleRecepcion.FirstOrDefault(dv => dv.ComReferencia == result.ComReferencia.ToString());
            if (encontrado == null)
            {
                DetalleRecepcion.Add(new RecepcionDetalleDTO
                {
                    ComReferencia = encontrado.ComReferencia,
                    NombreProducto = encontrado.NombreProducto,
                    PesoCompra = encontrado.PesoCompra

                });
                MostarTotal();
            }

        }

        public async Task GetRecepciones()
        {
            LoadingEsVisible = true;

            var lstRecepciones = await _context.DetalleCompras
            .ToListAsync();

            //await Task.Run(async () =>
            //{

            var lstTemp = new ObservableCollection<RecepcionDetalleDTO>();

            if (lstRecepciones.Any())
            {
                foreach (var item in lstRecepciones)
                {
                    lstTemp.Add(new RecepcionDetalleDTO
                    {
                        ComReferencia = "010101", //item.ComReferencia,
                        NombreProducto = "Producto Principal" ,//item.Producto?.ProDescripcion,
                        PesoCompra = item.Peso
                    });
                }

            }

            DetalleRecepcion = lstTemp;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                LoadingEsVisible = false;
                MostarTotal();
            });
            //});

        }

        private void CompraMensajeRecibido(CompraResult result)
        {
            CodigoCompra = result.Compra.CodigoCompra;
            IdVendedor = result.Compra.IdVendedor;
            NombreVendedor = result.Compra.Vendedor;
            NombreRepresentante = result.Compra.Representante;
            TotalpesoCompra = result.Compra.Total;
        }

        private void RecepcionDetalleMensajeRecibido(CompraDetalleResult result)
        {
            DetalleRecepcion.Add(new RecepcionDetalleDTO
            {
                ComReferencia = result.CompraDetalle.ComReferencia,
                NombreProducto = result.CompraDetalle.Producto.ProDescripcion,
                PesoCompra = result.CompraDetalle.Peso

            });
            MostarTotal();

        }
        [RelayCommand]
        private async Task Buscar()
        {
            LoadingEsVisible = true;

            await Shell.Current.Navigation.PushAsync(new ProductoPage(new ProductoVM(new DataAccess.VentaDbContext()), 0));

            await Task.Run(async () =>
            {
                await GetRecepciones();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    LoadingEsVisible = false;
                });
            });
        }

        [RelayCommand]
        private async Task Limpiar()
        {
            LoadingEsVisible = true;

            BuscarRecepcion = "";
            await Task.Run(async () =>
            {
                await GetRecepciones();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    LoadingEsVisible = false;
                });
            });

        }
        [RelayCommand]
        private void EliminarEvent(RecepcionDetalleDTO detalle)
        {
            DetalleRecepcion.Remove(detalle);
            MostarTotal();
        }

        public void MostarTotal()
        {
            TotalpesoCompra = DetalleRecepcion.Sum(c => c.PesoCompra);
            Totalpesorecibido = DetalleRecepcion.Sum(c => c.PesoRecibido);
        }

        [RelayCommand]
        private async Task FinalizarVenta()
        {
            if (DetalleRecepcion.Count < 1)
            {
                await Shell.Current.DisplayAlert("Mensaje", "No se han agregados productos a la recepcion", "Aceptar");
                return;
            }


            //if (Totalpesorecibido == 0)
            //{
            //    await Shell.Current.DisplayAlert("Mensaje", "Debe ingresar compras de productos", "Aceptar");
            //    return;
            //}

            //if (TotalpesoCompra > Totalpesorecibido)
            //{
            //    await Shell.Current.DisplayAlert("Mensaje", "Desea cerra la recepcion con productos pendintes?", "Si","No");
            //    return;
            //}

            //string nombreCliente = await Shell.Current.DisplayPromptAsync("Información del cliente", "Nombres:", accept: "Continuar", cancel: "Volver", placeholder: "(opcional)");

            try
            {
                List<RecepcionDetalle> detalleRecepcion = new List<RecepcionDetalle>();
                foreach (var item in DetalleRecepcion)
                {
                    detalleRecepcion.Add(new RecepcionDetalle
                    {
                        ComReferencia = item.ComReferencia,
                        PesoRecibido = item.PesoRecibido
                    });
                }

                Recepcion recepcion = new Recepcion()
                {
                    IdRepresentante = IdRepresentante,
                    FechaRecepcion = DateTime.Now,
                    IdVendedor = IdVendedor,
                    PesoCompra = TotalpesoCompra,
                    PesoRecibido = TotalpesoCompra,
                    recepcionDetalle = detalleRecepcion

                };

                _context.Recepciones.Add(recepcion);
                await _context.SaveChangesAsync();

                await Shell.Current.DisplayAlert("Listo!", $"Recepcion {recepcion.Id}' fue concluido!", "Aceptar");

                await Shell.Current.Navigation.PopAsync();

                //DetalleRecepcion.Clear();
                //TotalpesoCompra = 0;
                //Totalpesorecibido = 0;

                //MostarTotal();
            }
            catch
            {
                await Shell.Current.DisplayAlert("Error!", "No se pudo registrar la recepcion", "Aceptar");
            }
        }

        [RelayCommand]
        private async Task Volver()
        {
            LoadingEsVisible = true;
            await Shell.Current.Navigation.PopAsync();
        }

    }
}
