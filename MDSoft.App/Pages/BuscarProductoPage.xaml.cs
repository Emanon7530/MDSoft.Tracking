using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class BuscarProductoPage : ContentPage
{
	public BuscarProductoPage(BuscarProductoVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}