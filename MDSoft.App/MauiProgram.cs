using Tracking.Pages;
using Microsoft.Extensions.Logging;
using Tracking.DataAccess;
using Tracking.ViewModels;
using CommunityToolkit.Maui;
using MDSoft.Tracking.Services.AutoMapper;
using MDSoft.Tracking;
using Microsoft.EntityFrameworkCore;
using MDSoft.Tracking.Model;

namespace Tracking
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                //.UseMauiCameraView()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "FaSolid");
                });

            builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

            builder.Services.AddDbContext<VentaDbContext>();
            builder.Services.AddDbContext<MovilBusiness5StdContext>(options => options.UseSqlServer("Data Source=AGTDEVL1019;Trust Server Certificate=True;Initial Catalog=MovilBusiness5STD;uid=sa;pwd=manonram"));

            builder.Services.AddTransient<ProductoPage>();
            builder.Services.AddTransient<ProductoVM>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainVM>();

            builder.Services.AddTransient<RecepcionPage>();
            builder.Services.AddTransient<RecepcionVM>();

            builder.Services.AddTransient<RecepcionListPage>();
            builder.Services.AddTransient<RecepcionlistMV>();

            builder.Services.AddTransient<PesoLinealPage>();
            builder.Services.AddTransient<PesoLinealVM>();

            builder.Services.AddTransient<SettingDataPage>();
            builder.Services.AddTransient<SettingDataMV>();

            //builder.Services.AddSingleton<MainPage>();

            var dbContext = new VentaDbContext();
            var result = dbContext.Database.EnsureCreated();
            dbContext.Dispose();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            Routing.RegisterRoute(nameof(ProductoPage), typeof(ProductoPage));
            Routing.RegisterRoute(nameof(RecepcionListPage), typeof(RecepcionListPage));
            Routing.RegisterRoute(nameof(RecepcionPage), typeof(RecepcionPage));
            Routing.RegisterRoute(nameof(PesoLinealPage), typeof(PesoLinealPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SettingDataPage), typeof(SettingDataPage));

            return builder.Build();
        }
    }
}