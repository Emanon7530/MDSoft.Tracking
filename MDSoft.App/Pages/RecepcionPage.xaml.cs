using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class RecepcionPage : ContentPage
{

	private readonly RecepcionVM viewModel;
    private readonly int _idRecepcion;
	public RecepcionPage(RecepcionVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

	public RecepcionPage(RecepcionVM vm, int idRecepcion)
	{
		InitializeComponent();
		BindingContext = vm;
		_idRecepcion = idRecepcion;
		viewModel = vm;
    }

}