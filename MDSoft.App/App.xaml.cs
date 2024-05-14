using MDSoft.Tracking.Services.DTO;
using Tracking.Pages;
using Tracking.Services;
namespace Tracking
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            var logueado = Preferences.Get("logueado", string.Empty);
            if (string.IsNullOrEmpty(logueado))
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new AppShell();
                //MainPage = new RecepcionPage(new ViewModels.RecepcionVM(new ComprasProductoDTO()), "jlanda");
            }
        }
    }

}