using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tracking.Pages;
using Tracking.Utilidades;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using Tracking.DTOs;
using Tracking.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tracking.Modelos;

namespace Tracking.ViewModels
{
    public partial class InventarioVM : ObservableObject
    {
        private readonly VentaDbContext _context;
        public InventarioVM(VentaDbContext context)
        {
            WeakReferenceMessenger.Default.Register<ProductoMessage>(this, (r, m) =>
            {
                ProductoMensajeRecibido(m.Value);
            });

            _context = context;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(async () => await ObtenerCategorias());
                await Task.Run(async () => await ObtenerProductos());
            });
            PropertyChanged += InventarioVM_PropertyChanged;
        }

        private void InventarioVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BuscarProducto))
            {
                if (BuscarProducto != "")
                    BtnLimpiarEsVisible = true;
                else
                    BtnLimpiarEsVisible = false;
            }
        }

        [ObservableProperty]
        private CategoriaDTO categoriaSeleccionada;

        [ObservableProperty]
        ObservableCollection<CategoriaDTO> listaCategoria = new ObservableCollection<CategoriaDTO>();

        [ObservableProperty]
        private string buscarProducto = string.Empty;

        [ObservableProperty]
        private bool loadingEsVisible = false;
        [ObservableProperty]
        private bool loadingCategoriaEsVisible = false;

        [ObservableProperty]
        private bool dataEsVisible = false;

        [ObservableProperty]
        private bool btnLimpiarEsVisible = false;

        [ObservableProperty]
        ObservableCollection<ProductoDTO> listaProductos = new ObservableCollection<ProductoDTO>();

        private async Task ObtenerCategorias()
        {
            LoadingCategoriaEsVisible = true;
            await Task.Run(async () =>
            {
                var lstCategoria = await _context.Categorias.ToListAsync();
                var lstTemp = new ObservableCollection<CategoriaDTO>();
                var categoriaDefault = new CategoriaDTO { IdCategoria = 0, Nombre = "Todas las categorias" };
                lstTemp.Add(categoriaDefault);
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
                    ListaCategoria = lstTemp;
                    CategoriaSeleccionada = categoriaDefault;
                    LoadingCategoriaEsVisible = false;
                });
            });
        }

        public async Task ObtenerProductos()
        {
            DataEsVisible = false;
            LoadingEsVisible = true;

            await Task.Run(async () =>
            {
                var lstProductos = await _context.Productos
                //.Include(c => c.RefCategoria)
                .ToListAsync();
                var lstTemp = new ObservableCollection<ProductoDTO>();

                if (lstProductos.Any())
                {
                    foreach (var item in lstProductos)
                    {
                        lstTemp.Add(new ProductoDTO
                        {
                            ProId = item.ProId,
                            ProCodigo = item.ProCodigo,
                            ProDescripcion = item.ProDescripcion,
                            //Categoria = new CategoriaDTO() { IdCategoria = item.RefCategoria.IdCategoria, Nombre = item.RefCategoria.Nombre },
                            ProCantidad = item.ProCantidad,
                            ProPrecio = item.ProPrecio
                        });
                    }

                }
                ListaProductos = lstTemp;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DataEsVisible = true;
                    LoadingEsVisible = false;
                });
            });

        }

        [RelayCommand]
        private void Filtrar()
        {


        }

        [RelayCommand]
        private async Task Buscar()
        {
            //DataEsVisible = false;
            //LoadingEsVisible = true;

            //await Task.Run(async () =>
            //{
            //    ObservableCollection<ProductoDTO> encontrados = new ObservableCollection<ProductoDTO>();
            //    List<Producto> bdListCategorias = new List<Producto>();
            //    if (CategoriaSeleccionada.IdCategoria == 0)
            //        bdListCategorias = await _context.Productos.Where(p => p.Nombre.ToLower().Contains(BuscarProducto.ToLower())).ToListAsync();
            //    else
            //        bdListCategorias = await _context.Productos.Where(p => p.Nombre.ToLower().Contains(BuscarProducto.ToLower()) && p.IdCategoria == CategoriaSeleccionada.IdCategoria).ToListAsync();

            //    foreach (var item in bdListCategorias)
            //    {
            //        encontrados.Add(new ProductoDTO
            //        {
            //            IdProducto = item.IdProducto,
            //            Codigo = item.Codigo,
            //            Nombre = item.Nombre,
            //            Categoria = new CategoriaDTO { IdCategoria = item.RefCategoria.IdCategoria, Nombre = item.RefCategoria.Nombre },
            //            Cantidad = item.Cantidad,
            //            Precio = item.Precio
            //        });
            //    }

            //    ListaProductos = encontrados;
            //    MainThread.BeginInvokeOnMainThread(() =>
            //    {
            //        DataEsVisible = true;
            //        LoadingEsVisible = false;
            //    });
            //});
        }

        [RelayCommand]
        private async Task Limpiar()
        {
            DataEsVisible = false;
            LoadingEsVisible = true;

            BuscarProducto = "";
            await Task.Run(async () =>
            {
                await ObtenerProductos();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DataEsVisible = true;
                    LoadingEsVisible = false;
                });
            });

        }

        [RelayCommand]
        private async Task IrProducto()
        {
            await Shell.Current.Navigation.PushModalAsync(new ProductoPage(new ProductoVM(new DataAccess.VentaDbContext()), 0));
        }

        private void ProductoMensajeRecibido(ProductoResult result)
        {
            if (result.esCrear)
            {
                ListaProductos.Add(result.producto);
            }
            else
            {

                var prod = ListaProductos.First(p => p.ProId == result.producto.ProId);

                prod.ProCodigo = result.producto.ProCodigo;
                prod.ProDescripcion = result.producto.proDescripcion;
                //prod.Categoria = result.producto.Categoria;
                prod.ProCantidad = result.producto.ProCantidad;
                prod.ProPrecio = result.producto.ProPrecio;

            }
        }

        [RelayCommand]
        private async Task Editar(ProductoDTO producto)
        {
            await Shell.Current.Navigation.PushModalAsync(new ProductoPage(new ProductoVM(new DataAccess.VentaDbContext()), producto.ProId));
        }

        [RelayCommand]
        private async Task Eliminar(ProductoDTO producto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar el producto?", "Si, continuar", "No, volver");
            if (answer)
            {
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
                    var prod = await _context.Productos.FirstAsync(p => p.ProId == producto.ProId);
                    _context.Productos.Remove(prod);
                    await _context.SaveChangesAsync();
                    MainThread.BeginInvokeOnMainThread(() => {
                        LoadingEsVisible = false;
                        ListaProductos.Remove(producto);
                    });
                });
              
            }
        }
    }
}
