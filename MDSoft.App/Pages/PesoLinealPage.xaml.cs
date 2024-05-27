using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class PesoLinealPage : ContentPage
{
	private readonly PesoLinealVM viewModel;
    private readonly int _idProducto;
	public PesoLinealPage(PesoLinealVM vm, int idProducto)
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

    //protected override void OnDisappearing()
    //{
    //    viewModel.Desuscribir();
    //}
}