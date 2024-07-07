using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MDSoft.Tracking.Services.DTO;
using System.Collections.ObjectModel;
using Tracking.DataAccess;
using Tracking.Pages;
using Tracking.Services;
using Tracking.Utilidades;

namespace Tracking.ViewModels
{
    public partial class RecepcionlistMV : ObservableObject
    {
        private readonly TrackingDbContext _context;
        private readonly IAPIManager _apiManager;
        public RecepcionlistMV(TrackingDbContext context)
        {
            try
            {

                WeakReferenceMessenger.Default.Register<RecepcionCompraResult>(this, (r, m) =>
                 {
                     RecepcionMensajeRecibido(m);
                 });

                _context = context;
                _apiManager = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IAPIManager>();

                //TODO Agregar un Refreshview
                Task.Run(async () => await GetRecepciones());

                PropertyChanged += RecepcionVM_PropertyChanged;
            }
            catch (Exception e)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Shell.Current.DisplayAlert("Information", "Ups, Algo no salio como esperaba\n" + e.Message, "OK");
                });
            }

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
        private string buscarRecepcion = string.Empty;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        [ObservableProperty]
        private bool isRefreshing = false;

        [ObservableProperty]
        private bool dataEsVisible = false;

        [ObservableProperty]
        private bool btnLimpiarEsVisible = false;

        [ObservableProperty]
        ObservableCollection<ComprasProductoDTO> listRecepcion = new ObservableCollection<ComprasProductoDTO>();

        public async Task GetRecepciones()
        {
            try
            {

                IsRefreshing = true;
                LoadingEsVisible = true;

                var compras = await _apiManager.sp_GetComprasPendientes();

                ListRecepcion = new ObservableCollection<ComprasProductoDTO>(compras.OrderBy(x => x.ComFecha));

            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Information", "Ups, Algo no salio como esperaba\n" + e.Message, "OK");
            }
            finally
            {
                IsRefreshing = false;
                LoadingEsVisible = false;
            }
        }

        [RelayCommand]
        private async Task RefreshGrid()
        {
            await GetRecepciones();
        }

        [RelayCommand]
        private async Task Buscar()
        {
            DataEsVisible = false;
            LoadingEsVisible = true;

            try
            {

                var codigo = BuscarRecepcion.Split("-");
                var compras = await _apiManager.GetCompraByTicket(codigo[0], int.Parse(codigo[1]));

                if (compras != null)
                {
                    var lstTemp = new ObservableCollection<ComprasProductoDTO>();
                    lstTemp.Add(new ComprasProductoDTO
                    {
                        ComReferencia = compras.ComReferencia,
                        RepCodigo = compras.RepCodigo,
                        RepSupervisor = compras.RepSupervisor,
                        ComSecuencia = compras.ComSecuencia,
                        ComFecha = compras.ComFecha,
                        ComCantidadDetalle = compras.ComCantidadDetalle,
                        ComEstatus = compras.ComEstatus,
                        RepNombre = compras.RepNombre
                    });

                    ListRecepcion = lstTemp;

                    await IrRecepcion(lstTemp[0]);
                }
                else
                {
                    ListRecepcion.Clear();
                }

            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Information", "Ups, Algo no salio como esperaba\n" + e.Message, "OK");
            }
            finally
            {
                IsRefreshing = false;
                LoadingEsVisible = false;
            }
        }

        [RelayCommand]
        private async Task Limpiar()
        {
            DataEsVisible = false;
            LoadingEsVisible = true;

            BuscarRecepcion = "";

            await GetRecepciones();

            DataEsVisible = true;
            LoadingEsVisible = false;
        }

        [RelayCommand]
        private async Task IrRecepcion(ComprasProductoDTO recepcion)
        {
            DataEsVisible = false;
            LoadingEsVisible = true;

            try
            {

                await Shell.Current.Navigation.PushAsync(new RecepcionPage(new RecepcionVM(recepcion), recepcion.RepCodigo));

                await GetRecepciones();
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Information", "Ups, Algo no salio como esperaba\n" + e.Message, "OK");
            }
            finally
            {
                IsRefreshing = false;
                LoadingEsVisible = false;
            }

        }

        private void RecepcionMensajeRecibido(RecepcionCompraResult result)
        {
            //TODO Agregar un Refreshview
            Task.Run(async () => await GetRecepciones());
        }

        [RelayCommand]
        private async Task Editar(ComprasProductoDTO recepcion)
        {
            await Shell.Current.Navigation.PushModalAsync(new RecepcionListPage(new RecepcionlistMV(new DataAccess.TrackingDbContext())));//, recepcion.Id));
        }

        [RelayCommand]
        private async Task Eliminar(ComprasProductoDTO recepcion)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar esta recepción?", "Si, continuar", "No, volver");
            if (answer)
            {
                LoadingEsVisible = true;
                await Task.Run(async () =>
                {
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
