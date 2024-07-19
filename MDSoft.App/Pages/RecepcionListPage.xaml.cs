using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class RecepcionListPage : ContentPage
{
	public RecepcionListPage(RecepcionListMV viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}