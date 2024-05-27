using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class SettingDataPage : ContentPage
{
	public SettingDataPage(SettingDataMV viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}