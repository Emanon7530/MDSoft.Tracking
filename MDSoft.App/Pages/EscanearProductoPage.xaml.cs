using Tracking.DataAccess;
using Tracking.DTOs;
using Tracking.Modelos;
using Tracking.Utilidades;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Tracking.Pages;

public partial class EscanearProductoPage : ContentPage
{
    private readonly VentaDbContext _context;
	public EscanearProductoPage(VentaDbContext context)
	{
		InitializeComponent();
        cameraView.BarCodeOptions = new Camera.MAUI.ZXingHelper.BarcodeDecodeOptions()
        {
            TryHarder = true,
            PossibleFormats = { ZXing.BarcodeFormat.All_1D }
        };
        _context = context;
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(new Action(async () =>
            {

                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            }));
        }
    }

    private async void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            string codigo = args.Result[0].Text;
            Producto dbProducto = await _context.Productos
            //.Include(c => c.RefCategoria)
            .FirstOrDefaultAsync(p => p.ProCodigo == codigo);
            ProductoDTO producto = new ProductoDTO()
            {
                ProId = dbProducto.ProId,
                ProCodigo = dbProducto.ProCodigo,
                ProDescripcion = dbProducto.ProDescripcion,
                //Categoria = new CategoriaDTO()
                //{
                //    IdCategoria = dbProducto.IdCategoria,
                //    Nombre = dbProducto.RefCategoria.Nombre
                //},
                ProCantidad = dbProducto.ProCantidad,
                ProPrecio = dbProducto.ProPrecio
            };
            WeakReferenceMessenger.Default.Send(new ProductoVentaMessage(producto));
        });

        await Shell.Current.Navigation.PopModalAsync();
    }
}