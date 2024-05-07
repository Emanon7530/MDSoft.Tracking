using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class HistoriaVentaPage : ContentPage
{
	public HistoriaVentaPage(HistorialVentaVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}