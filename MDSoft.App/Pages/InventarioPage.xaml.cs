using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class InventarioPage : ContentPage
{
	public InventarioPage(InventarioVM viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}