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
    public partial class RecepcionlistMV : ObservableObject
    {
        private readonly VentaDbContext _context;
        public RecepcionlistMV(VentaDbContext context)
        {
            WeakReferenceMessenger.Default.Register<RecepcionCompraMessage>(this, (r, m) =>
            {
                RecepcionMensajeRecibido(m.Value);
            });

            _context = context;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                //await Task.Run(async () => await ObtenerCategorias());
                await Task.Run(async () => await GetRecepciones());
            });
            PropertyChanged += RecepcionVM_PropertyChanged;
        }

        private void RecepcionVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BuscarRecepcion))
            {
                if (BuscarRecepcion != "")
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
        private string buscarRecepcion = string.Empty;

        [ObservableProperty]
        private bool loadingEsVisible = false;
        [ObservableProperty]
        private bool loadingCategoriaEsVisible = false;

        [ObservableProperty]
        private bool dataEsVisible = false;

        [ObservableProperty]
        private bool btnLimpiarEsVisible = false;

        [ObservableProperty]
        ObservableCollection<RecepcionDTO> listRecepcion = new ObservableCollection<RecepcionDTO>();

        private async Task ObtenerCategorias()
        {
            LoadingCategoriaEsVisible = true;
            await Task.Run(async () =>
            {
                var lstCategoria = await _context.Categorias.ToListAsync();
                var lstTemp = new ObservableCollection<CategoriaDTO>();
                var categoriaDefault = new CategoriaDTO { IdCategoria = 0, Nombre = "Todos los Lotes" };
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

        public async Task GetRecepciones()
        {
            DataEsVisible = false;
            LoadingEsVisible = true;

            await Task.Run(async () =>
            {
                var lstRecepciones = await _context.Recepciones.Include(c => c.recepcionDetalle).ToListAsync();
                var lstTemp = new ObservableCollection<RecepcionDTO>();

                if (lstRecepciones.Any())
                {
                    foreach (var item in lstRecepciones)
                    {
                        lstTemp.Add(new RecepcionDTO
                        {
                            Id = item.Id,
                            TipoLote = item.TipoLote,
                            IdVendedor = item.IdVendedor,
                            IdRepresentante = item.IdRepresentante,
                            CodigoCompra = item.CodigoCompra,
                            FechaRecepcion = item.FechaRecepcion,
                            LotFermentacion = item.LotFermentacion,
                            LotSecadoMaquina = item.LotSecadoMaquina,
                            NombreRepresentante = "Enriquillo Manon",
                            NombreVendedor = "Alberto de la Cruz",
                            PesoCompra = item.PesoCompra,
                            PesoRecibido = item.PesoRecibido
                            //Categoria = new CategoriaDTO() { IdCategoria = item.RefCategoria.IdCategoria, Nombre = item.RefCategoria.Nombre },
                        });
                    }

                }

                ListRecepcion = lstTemp;

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
            DataEsVisible = false;
            LoadingEsVisible = true;

            await Task.Run(async () =>
            {
                ObservableCollection<RecepcionDTO> encontrados = new ObservableCollection<RecepcionDTO>();
                List<Recepcion> bdListCategorias = new List<Recepcion>();
                if (CategoriaSeleccionada.IdCategoria == 0)
                    bdListCategorias = await _context.Recepciones.Where(p => p.CodigoCompra.ToLower().Contains(BuscarRecepcion.ToLower())).ToListAsync();
                else
                    bdListCategorias = await _context.Recepciones.Where(p => p.CodigoCompra.ToLower().Contains(BuscarRecepcion.ToLower()) && p.TipoLote == CategoriaSeleccionada.IdCategoria).ToListAsync();

                foreach (var item in bdListCategorias)
                {
                    encontrados.Add(new RecepcionDTO
                    {
                        TipoLote = item.TipoLote,
                        Id = item.Id,
                        IdVendedor = item.IdVendedor,
                        IdRepresentante = item.IdRepresentante,
                        CodigoCompra = item.CodigoCompra,
                        NombreVendedor = "Juan Alberto Gomez",
                        NombreRepresentante = "Carlos Jose Vidal",
                        FechaRecepcion = item.FechaRecepcion,
                        LotFermentacion = item.LotFermentacion,
                        LotSecadoMaquina = item.LotSecadoMaquina,
                        PesoCompra = item.PesoCompra,
                        PesoRecibido = item.PesoRecibido
                        //Categoria = new CategoriaDTO() { IdCategoria = item.RefCategoria.IdCategoria, Nombre = item.RefCategoria.Nombre },
                    });
                }

                ListRecepcion = encontrados;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DataEsVisible = true;
                    LoadingEsVisible = false;
                });
            });
        }

        [RelayCommand]
        private async Task Limpiar()
        {
            DataEsVisible = false;
            LoadingEsVisible = true;

            BuscarRecepcion = "";
            await Task.Run(async () =>
            {
                await GetRecepciones();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DataEsVisible = true;
                    LoadingEsVisible = false;
                });
            });

        }

        [RelayCommand]
        private async Task IrRecepcion()
        {
            DataEsVisible = false;
            LoadingEsVisible = true;

            await Shell.Current.Navigation.PushAsync(new RecepcionPage(new RecepcionVM(new DataAccess.VentaDbContext()), 0));

            await Task.Run(async () =>
            {
                await GetRecepciones();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DataEsVisible = true;
                    LoadingEsVisible = false;
                });
            });
        }

        private void RecepcionMensajeRecibido(RecepcionCompraResult result)
        {

            

        }

        [RelayCommand]
        private async Task Editar(RecepcionDTO recepcion)
        {
            await Shell.Current.Navigation.PushModalAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.VentaDbContext())));//, recepcion.Id));
        }

        [RelayCommand]
        private async Task Eliminar(RecepcionDTO recepcion)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar esta recepción?", "Si, continuar", "No, volver");
            if (answer)
            {
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
                    var prod = await _context.Recepciones.FirstAsync(p => p.Id == recepcion.Id);
                    _context.Recepciones.Remove(prod);

                    await _context.SaveChangesAsync();

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        LoadingEsVisible = false;
                        ListRecepcion.Remove(recepcion);
                    });
                });

            }
        }
    }
}
