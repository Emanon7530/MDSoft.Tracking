using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class CompraProductoDetallePage : ContentPage
{
    private readonly CompraProductoDetalleMV viewModel;
    private readonly int _idProducto;
    public CompraProductoDetallePage(CompraProductoDetalleMV vm, int idProducto)
    {
        InitializeComponent();
        BindingContext = vm;
        viewModel = vm;
        _idProducto = idProducto;

    }
    protected override void OnAppearing()
    {
        viewModel.Inicio(_idProducto);
    }

    protected override void OnDisappearing()
    {
        //viewModel.Desuscribir();
    }
}