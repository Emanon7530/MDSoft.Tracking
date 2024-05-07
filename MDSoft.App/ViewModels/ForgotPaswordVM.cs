using Tracking.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.ViewModels
{
    public partial class ForgotPaswordVM : ObservableObject
    {
        [ObservableProperty]
        public string email = string.Empty;

        [RelayCommand]
        private async Task Forgot()
        {
            if (!string.IsNullOrEmpty(email))
            {
                Preferences.Set("logueado", "");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Olvido Su Clave", "Revise su correo, se envio un link para resetear su calve", "Aceptar");
            }
            Application.Current.MainPage = new LoginPage();
        }


    }
}
