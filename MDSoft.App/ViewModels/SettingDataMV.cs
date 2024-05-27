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
    private readonly VentaDbContext _context;
    public SettingDataMV(VentaDbContext context)
    {
        //WeakReferenceMessenger.Default.Register<RecepcionCompraMessage>(this, (r, m) =>
        //{
        //    RecepcionMensajeRecibido(m.Value);
        //});

        _context = context;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
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

    public async Task GetSettings()
    {
        LoadingEsVisible = true;

        await Task.Run(async () =>
        {
            currentSetting = await _context.SettingData.FirstAsync();

            //if (currentSetting != null)
            //{
            //    MdsoftKey = currentSetting.mdsoftKey;
            //    UrlApi = currentSetting.urlApi;

            //    _context.Entry(currentSetting).State = EntityState.Modified;
            //}
            //else
            //{
            //currentSetting = new SettingsData();
            //await _context.SettingData.AddAsync(currentSetting);
            //}

            MainThread.BeginInvokeOnMainThread(() =>
            {
                LoadingEsVisible = false;
            });
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
            await Task.Run(async () =>
            {
                await _context.SaveChangesAsync();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    LoadingEsVisible = false;
                });
            });

        }
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        LoadingEsVisible = true;
        await Task.Run(async () =>
        {
            await Shell.Current.Navigation.PopModalAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                LoadingEsVisible = false;
            });
        });

    }
}

