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

namespace Tracking.ViewModels
{

    public partial class ProductoVM : ObservableObject
    {
        private readonly VentaDbContext _context;
        public ProductoVM(VentaDbContext context)
        {
            WeakReferenceMessenger.Default.Register<BarcodeScannedMessage>(this, (r, m) =>
            {
                BarcodeMensajeRecibido(m.Value);
            });
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
        private string numeroLote = string.Empty;

        [ObservableProperty]
        private int cantidad;

        [ObservableProperty]
        private decimal precio;

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
                    CodigoBarras = "010101"; // encontrado.ProCodigo;
                    Nombre = "Producto Principal" ;//encontrado.ProDescripcion;
                    Cantidad = (int)encontrado.ProCantidad.Value;
                    Precio = encontrado.ProPrecio.Value;

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        LoadingEsVisible = false;
                        //await ObtenerCategorias(encontrado.IdCategoria);
                    });

                });

            }
        }
        private void BarcodeMensajeRecibido(BarcodeResult result)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                CodigoBarras = result.BarcodeValue;
            });
        }

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
            await Shell.Current.Navigation.PushModalAsync(new BarcodePage());
        }

        [RelayCommand]
        private async Task AddLote()
        {
            string resultado = await Shell.Current.DisplayPromptAsync("Nuevo Lote", "Escane el Lote", accept: "Aceptar", cancel: "Cancelar");
            if (!string.IsNullOrEmpty(resultado))
            {
                //LoadingEsVisible = true;
                //await Task.Run(async () =>
                //{
                //    Categoria modelo = new Categoria
                //    {
                //        Nombre = resultado
                //    };
                //    _context.Categorias.Add(modelo);
                //    await _context.SaveChangesAsync();

                //    MainThread.BeginInvokeOnMainThread(() =>
                //    {
                //        //ListaCategorias.Add(new CategoriaDTO { IdCategoria = modelo.IdCategoria, Nombre = modelo.Nombre });
                //        LoadingEsVisible = false;
                //    });
                //});
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
                ProductoResult productoResult = new ProductoResult();

                var enviarProducto = new ProductoDTO
                {
                    ProId = IdProducto,
                    ProCodigo = CodigoBarras,
                    ProDescripcion = Nombre,
                    //Categoria = CategoriaSeleccionada,
                    ProCantidad = Cantidad,
                    ProPrecio = Precio
                };

                if (IdProducto == 0)
                {
                    var dbProducto = new Producto
                    {
                        ProId = IdProducto,
                        ProCodigo = CodigoBarras,
                        ProDescripcion = Nombre,
                        ProCantidad = Cantidad,
                        ProPrecio = Precio
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
                    encontrado.ProCantidad = Cantidad;
                    encontrado.ProPrecio = Precio;
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
