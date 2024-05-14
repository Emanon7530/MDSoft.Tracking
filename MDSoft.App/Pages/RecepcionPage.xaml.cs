using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class RecepcionPage : ContentPage
{

	private readonly RecepcionVM viewModel;
    private readonly string _idRecepcion;
	public RecepcionPage(RecepcionVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

	public RecepcionPage(RecepcionVM vm, string idRecepcion)
	{
		InitializeComponent();
		BindingContext = vm;
		_idRecepcion = idRecepcion;
		viewModel = vm;
    }

    protected override void OnAppearing()
    {
        viewModel.Inicio(_idRecepcion);
    }

}