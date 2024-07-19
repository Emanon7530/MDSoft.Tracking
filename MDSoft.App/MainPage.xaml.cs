using Tracking.Services;
using Tracking.ViewModels;

namespace Tracking
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }


    }
}