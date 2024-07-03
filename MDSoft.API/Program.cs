using AutoMapper;
using MDSoft.Tracking.API.Utilities;
using MDSoft.Tracking.Model;
using MDSoft.Tracking.Model.Model;
using MDSoft.Tracking.Services;
using MDSoft.Tracking.Services.AutoMapper;
using MDSoft.Tracking.Services.DTO;
using MDSoft.Tracking.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Text.Json.Serialization;

var _logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("https://movilbusiness.com.do/MDSOFT_Tracking/",
                                                  "https://10.0.2.2:7033/");
                          });
    });

    builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "MDSoft Layer Services",
            Description = "Layer Services for access to MDSoft Services",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Example Contact",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://example.com/license")
            }
        });
    });

    builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
    builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
    builder.Services.AddTransient<IMailService, MailService>();
    builder.Services.AddDbContext<MovilBusiness5StdContext>();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsProduction())
    //{
    //    app.UseSwagger();
    //    app.UseSwaggerUI();
    //}

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    /// api
    app.UseRouting();

    app.UseCors(MyAllowSpecificOrigins);

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception e)
{
    _logger.Error(e, "Program has stopped!");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
