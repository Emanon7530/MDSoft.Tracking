using Tracking.DataAccess;
using Tracking.DTOs;
using Tracking.Modelos;
using Tracking.Utilidades;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Tracking.Pages;

public partial class EscanearComrpaDetallePage : ContentPage
{
    private readonly VentaDbContext _context;
	public EscanearComrpaDetallePage(VentaDbContext context)
	{
		InitializeComponent();
        cameraView.BarCodeOptions = new Camera.MAUI.ZXingHelper.BarcodeDecodeOptions()
        {
            TryHarder = true,
            PossibleFormats = { ZXing.BarcodeFormat.All_1D }
        };
        _context = context;
    }

    private void CameraView_CamerasLoaded(object sender, EventArgs e)
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

    private async void CameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            string codigo = args.Result[0].Text;
            DetalleCompra dbcompraDetalle = await _context.DetalleCompras.Include(x=>x.Compra)
            .Include(c => c.Producto).FirstOrDefaultAsync(p => p.IdCompra == 1);

            CompraDetalleDTO compraDetalle = new CompraDetalleDTO()
            {
                ComReferencia = dbcompraDetalle.ComReferencia,
                IdCompra = dbcompraDetalle.IdCompra,
                Peso = dbcompraDetalle.Peso,
                Estatus = dbcompraDetalle.Estatus,
                
                Producto = new ProductoDTO()
                {
                    ProId = dbcompraDetalle.Producto.ProId,
                    ProDescripcion = dbcompraDetalle.Producto.ProDescripcion,
                    ProCantidad = 1,
                },
            };
            var result = new CompraDetalleResult() { 
                CompraDetalle = compraDetalle,
                esCrear=true
            };
            WeakReferenceMessenger.Default.Send(new CompraDetalleMessage(result));
        });

        await Shell.Current.Navigation.PopModalAsync();
    }

    private void AcceptButton_Clicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            string codigo = "010101010101";
            //DetalleCompra dbcompraDetalle = await _context.DetalleCompras.Include(x => x.Compra)
            //.Include(c => c.Producto).FirstOrDefaultAsync(p => p.IdCompra == 1);

            CompraDetalleDTO compraDetalle = new CompraDetalleDTO()
            {
                ComReferencia = "0101010101",
                IdCompra = 1,
                Peso = 20,
                Estatus = 1,

                Producto = new ProductoDTO()
                {
                    ProId = 1,
                    ProDescripcion = "Producto 1",
                    ProCantidad = 1,
                },
            };
            var result = new CompraDetalleResult()
            {
                CompraDetalle = compraDetalle,
                esCrear = true
            };
            WeakReferenceMessenger.Default.Send(new CompraDetalleMessage(result));
        });

        Shell.Current.Navigation.PopModalAsync();

    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PopModalAsync();
    }
}