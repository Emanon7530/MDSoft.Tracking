using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tracking.Pages;
using Tracking.Utilidades;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using Tracking.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tracking.Modelos;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services;
using System.Text.Json;
using AutoMapper;
using MDSoft.Tracking.Services.AutoMapper;
using Tracking.Handlers;
using Newtonsoft.Json;
using Tracking.Services;

namespace Tracking.ViewModels;
public partial class SettingDataMV : ObservableObject
{
    private readonly TrackingDbContext _context;
    public SettingDataMV(TrackingDbContext context)
    {
        _context = context;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (!await ValidAccess())
            {
                await Shell.Current.DisplayAlert("Access", "Clave Administrador invalido!", "OK");
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            await Task.Run(async () => await GetSettings());
        });
    }

    [ObservableProperty]
    private string mdsoftKey;

    [ObservableProperty]
    private string urlApi;

    [ObservableProperty]
    private bool loadingEsVisible = false;

    private SettingsData currentSetting;

    public async Task<bool> ValidAccess()
    {

        bool isValid = false;
        var answer = await Shell.Current.DisplayPromptAsync("Access", "Digite la clave Administración", "Aceptar", "Cancelar");

        if (answer == "P@ssword1")
        {
            isValid = true;
        }

        return isValid;
    }

    public async Task GetSettings()
    {
        LoadingEsVisible = true;

        currentSetting = await _context.SettingData.FirstOrDefaultAsync();

        if (currentSetting != null)
        {
            MdsoftKey = currentSetting.mdsoftKey;
            UrlApi = currentSetting.urlApi;

            _context.Entry(currentSetting).State = EntityState.Modified;
        }
        else
        {
            currentSetting = new SettingsData();
            await _context.SettingData.AddAsync(currentSetting);
        }

        MainThread.BeginInvokeOnMainThread(() =>
        {
            LoadingEsVisible = false;
        });
    }

    private void RecepcionMensajeRecibido(RecepcionCompraResult result)
    {

    }

    [RelayCommand]
    private async Task Guardar()
    {
        bool answer = await Shell.Current.DisplayAlert("Settings", "Desea Guardar su nueva configuración?", "Si, continuar", "No, volver");

        if (answer)
        {
            LoadingEsVisible = true;

            currentSetting.mdsoftKey = MdsoftKey;
            currentSetting.urlApi = UrlApi;

            await _context.SaveChangesAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                LoadingEsVisible = false;
                Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            });
        }
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        LoadingEsVisible = true;
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");

        MainThread.BeginInvokeOnMainThread(() =>
        {
            LoadingEsVisible = false;
        });
    }
}

