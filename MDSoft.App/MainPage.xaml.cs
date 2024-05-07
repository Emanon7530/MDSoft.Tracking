using Tracking.ViewModels;

namespace Tracking
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        
    }
}