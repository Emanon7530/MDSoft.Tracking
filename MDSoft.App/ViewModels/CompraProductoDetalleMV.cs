using Tracking.DataAccess;
using Tracking.DTOs;
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

namespace Tracking.ViewModels
{

    public partial class CompraProductoDetalleMV : ObservableObject
    {
        private readonly VentaDbContext _context;
        public CompraProductoDetalleMV(VentaDbContext context)
        {
            //WeakReferenceMessenger.Default.Register<BarcodeScannedMessage>(this, (r, m) =>
            //{
            //    BarcodeMensajeRecibido(m.Value);
            //});
            _context = context;
        }

        private int IdProducto;
        [ObservableProperty]
        private bool loadingEsVisible = false;

        [ObservableProperty]
        private string codigoBarras = string.Empty;

        [ObservableProperty]
        private string nombre = string.Empty;

        [ObservableProperty]
        private CategoriaDTO categoriaSeleccionada;

        [ObservableProperty]
        private int loteDestino;

        [ObservableProperty]
        private string loteComReference = string.Empty;

        [ObservableProperty]
        private decimal pesoCompra;

        [ObservableProperty]
        private decimal pesoRecepcion;

        [ObservableProperty]
        ObservableCollection<CategoriaDTO> listaCategoria;

        [ObservableProperty]
        private string tituloPagina;

        public async void Inicio(int idProducto)
        {
            IdProducto = idProducto;



            if (IdProducto == 0)
            {
                TituloPagina = "Agregar producto";
                await ObtenerCategorias();
            }
            else
            {
                TituloPagina = "Editar producto";
                LoadingEsVisible = true;

                await Task.Run(async () =>
                {
                    var encontrado = await _context.Productos.FirstAsync(p => p.ProId == IdProducto);
                    CodigoBarras = encontrado.ProCodigo;
                    Nombre = encontrado.ProDescripcion;
                    PesoCompra = encontrado.ProCantidad.Value;
                    PesoRecepcion = encontrado.ProPrecio.Value;

                    //MainThread.BeginInvokeOnMainThread(async () =>
                    //{
                    //    LoadingEsVisible = false;
                    //    await ObtenerCategorias(encontrado.IdCategoria);
                    //});

                });


            }
        }
        //private void BarcodeMensajeRecibido(BarcodeResult result)
        //{
        //    MainThread.BeginInvokeOnMainThread(() =>
        //    {
        //        CodigoBarras = result.BarcodeValue;
        //    });
        //}

        private async Task ObtenerCategorias(int idCategoria = 0)
        {
            LoadingEsVisible = true;
            await Task.Run(async () =>
            {
                var lstCategoria = await _context.Categorias.ToListAsync();
                var lstTemp = new ObservableCollection<CategoriaDTO>();
                foreach (var item in lstCategoria)
                {
                    lstTemp.Add(new CategoriaDTO
                    {
                        IdCategoria = item.IdCategoria,
                        Nombre = item.Nombre
                    });
                }
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    LoadingEsVisible = false;
                    ListaCategoria = lstTemp;
                    if (idCategoria != 0)
                        CategoriaSeleccionada = ListaCategoria.First(c => c.IdCategoria == idCategoria);
                });
            });
        }

        [RelayCommand]
        private async Task MostrarScanner()
        {
            //await Shell.Current.Navigation.PushModalAsync(new BarcodePage());
        }

        [RelayCommand]
        private async Task VolverInventario()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        [RelayCommand]
        private async Task Guardar()
        {
            LoadingEsVisible = true;
            ProductoResult productoResult = new ProductoResult();

            var enviarProducto = new ProductoDTO
            {
                ProId = IdProducto,
                ProCodigo = CodigoBarras,
                ProDescripcion = Nombre,
                //Categoria = CategoriaSeleccionada,
                ProCantidad = (int)PesoCompra,
                ProPrecio = PesoRecepcion
            };

            await Task.Run(async () =>
            {

                if (IdProducto == 0)
                {
                    var dbProducto = new Producto
                    {
                        ProId = IdProducto,
                        ProCodigo = CodigoBarras,
                        ProDescripcion = Nombre,
                        //IdCategoria = CategoriaSeleccionada.IdCategoria,
                        ProCantidad = (int)PesoCompra,
                        ProPrecio = PesoRecepcion
                    };
                    _context.Productos.Add(dbProducto);

                    await _context.SaveChangesAsync();

                    enviarProducto.ProId = dbProducto.ProId;

                    productoResult = new ProductoResult()
                    {
                        esCrear = true,
                        producto = enviarProducto
                    };

                }
                else
                {
                    var encontrado = await _context.Productos.FirstAsync(p => p.ProId == IdProducto);
                    encontrado.ProId = IdProducto;
                    encontrado.ProCodigo = CodigoBarras;
                    encontrado.ProDescripcion = Nombre;
                    //encontrado.IdCategoria = CategoriaSeleccionada.IdCategoria;
                    encontrado.ProCantidad = (int)PesoCompra;
                    encontrado.ProPrecio = PesoRecepcion;
                    _context.Productos.Update(encontrado);
                    await _context.SaveChangesAsync();

                    productoResult = new ProductoResult()
                    {
                        esCrear = false,
                        producto = enviarProducto
                    };
                }
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new ProductoMessage(productoResult));
                    await Shell.Current.Navigation.PopModalAsync();
                });
            });

        }

    }
}
