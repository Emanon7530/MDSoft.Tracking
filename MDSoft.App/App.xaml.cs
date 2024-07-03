using __XamlGeneratedCode__;
using MDSoft.Tracking.Services.DTO;
using System.Runtime.ExceptionServices;
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
                //MainPage = new TestPage();
            }
            else
            {
                //MainPage = new TestPage();
                MainPage = new AppShell();
            }
        }
    }
}
