using Tracking.ViewModels;

namespace Tracking.Pages;

public partial class CategoriasPage : ContentPage
{
    public CategoriasPage(CategoriasVM viewmodel)
	{
		InitializeComponent();
        BindingContext = viewmodel;
    }

}