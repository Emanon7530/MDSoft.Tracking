using Tracking.DataAccess;
using Tracking.DTOs;
using Tracking.Modelos;
using Tracking.Utilidades;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Tracking.Pages;

public partial class EscanearComprasPage : ContentPage
{
    private readonly VentaDbContext _context;
    public EscanearComprasPage(VentaDbContext context)
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
            Compra dbCompra = await _context.Compras.Include(c => c.detalleCompra).FirstOrDefaultAsync(p => p.CodigoCompra == codigo);
            CompraDTO compra = new CompraDTO()
            {
                Id = dbCompra.Id,
                IdRepresentante = dbCompra.IdRepresentante,
                IdVendedor = dbCompra.IdVendedor,
                Fecha = dbCompra.Fecha,
                Peso = dbCompra.Peso,
            };

            compra.compraDetalle = new List<CompraDetalleDTO>();

            foreach (var item in dbCompra.detalleCompra)
            {
                compra.compraDetalle.Add(new CompraDetalleDTO()
                {
                    IdCompra = compra.Id,
                    Peso = item.Peso,
                    ComReferencia = item.ComReferencia,
                    Estatus = item.Estatus,
                    Producto = new ProductoDTO()
                    {
                        ProCodigo = item.Producto.ProCodigo,
                        ProDescripcion = item.Producto.ProDescripcion
                    }
                });
            };
            var compraresult = new CompraResult()
            {
                esCrear = true,
                Compra = compra
            };
            WeakReferenceMessenger.Default.Send(new CompraMessage(compraresult));
        });

        await Shell.Current.Navigation.PopModalAsync();
    }

    private void AcceptButton_Clicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            string codigo = "0101010101";

            Compra dbCompra = await _context.Compras.Include(c => c.detalleCompra).FirstOrDefaultAsync(p => p.CodigoCompra == codigo);

            CompraDTO compra = new CompraDTO()
            {
                Id = dbCompra.Id,
                IdRepresentante = dbCompra.IdRepresentante,
                Vendedor = "Milquiadez Dias",
                Representante="Enriquillo Manon",
                IdVendedor = dbCompra.IdVendedor,
                Fecha = dbCompra.Fecha,
                Peso = dbCompra.Peso,
            };

            compra.compraDetalle = new List<CompraDetalleDTO>();

            foreach (var item in dbCompra.detalleCompra)
            {
                compra.compraDetalle.Add(new CompraDetalleDTO()
                {
                    IdCompra = compra.Id,
                    Peso = item.Peso,
                    ComReferencia = item.ComReferencia,
                    Estatus = item.Estatus,
                    Producto = new ProductoDTO()
                    {
                        ProCodigo = item.Producto.ProCodigo,
                        ProDescripcion = item.Producto.ProDescripcion
                    }
                });
            };

            var compraresult = new CompraResult()
            {
                esCrear = true,
                Compra = compra
            };
            WeakReferenceMessenger.Default.Send(new CompraMessage(compraresult));
        });

        Shell.Current.Navigation.PopModalAsync();
    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {

        Shell.Current.Navigation.PopModalAsync();
    }
}