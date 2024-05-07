using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class RecepcionListPage : ContentPage
{
	public RecepcionListPage(RecepcionlistMV viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}