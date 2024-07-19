using Tracking.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDSoft.Tracking.Services.Exceptions;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Services.DTO;
using Newtonsoft.Json;
using Tracking.Utilidades;

namespace Tracking.ViewModels
{
    public partial class LoginVM : ObservableObject
    {
        [ObservableProperty]
        public string usuario = string.Empty;
        [ObservableProperty]
        public string password = string.Empty;
        [ObservableProperty]
        public string fullName = string.Empty;

        [RelayCommand]
        private async Task Login()
        {

            UsuarioSistemaDTO userfound;
            try
            {
                if (string.IsNullOrEmpty(Usuario))
                {
                    throw new InvalidUsernameOrPasswordException();
                }

                if (string.IsNullOrEmpty(Password))
                {
                    throw new InvalidUsernameOrPasswordException();
                }

                Preferences.Set("logueado", "si");
                Preferences.Set("usuario", Usuario);

                Application.Current.MainPage = new AppShell();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", $"Algo no salio bien, {ex.Message}", "Aceptar");
            }

        }
    }
}
