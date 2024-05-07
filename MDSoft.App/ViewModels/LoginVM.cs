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

                using (var client = new HttpClient())
                {
                    //string urlBase = $"{Util.urlBase}/Seguridad/Login?Username={Usuario}&Password={Password}";

                    ///// https://localhost:7033/api/v1/
                    //client.BaseAddress = new Uri(urlBase);
                    //HttpResponseMessage response = await client.GetAsync(urlBase);

                    //if (response.IsSuccessStatusCode)
                    //{
                    //    string content = response.Content.ReadAsStringAsync().Result;
                    //    userfound = JsonConvert.DeserializeObject<UsuarioSistemaDTO>(content);
                    Preferences.Set("logueado", "si");
                    Application.Current.MainPage = new AppShell();
                    //}
                    //else
                    //{
                    //    throw new InvalidUsernameOrPasswordException();
                    //    //await Application.Current.MainPage.DisplayAlert("Mensaje", "No se encontraron coincidencias", "Aceptar");
                    //}
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", $"Algo no salio bien, {ex.Message}", "Aceptar");
            }

        }

        [RelayCommand]
        private async Task ForgetPassword()
        {
            Preferences.Set("logueado", "si");
            Application.Current.MainPage = new ForgotPasswordPage();
        }

        [RelayCommand]
        private async Task Register()
        {
            if (Usuario == "Admin" && Password == "123")
            {
                Preferences.Set("logueado", "si");
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Mensaje", "No se encontraron coincidencias", "Aceptar");
            }
        }


    }
}
