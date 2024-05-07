using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class VentaPage : ContentPage
{
	public VentaPage(VentaVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}