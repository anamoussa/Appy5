using Appy.ViewModels.Dashboard;
using Appy.ViewModels.Startup;
using Appy.Views.Dashboard;
using Appy.Views.Startup;
using Camera.MAUI;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using BarcodeScanner.Mobile;
using Appy.Views;
using Appy.ViewModels;

namespace Appy;

public static class MauiProgram
{


    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
              .ConfigureMauiHandlers(handlers =>
              {
                  // Add the handlers
                  handlers.AddBarcodeScannerHandler();
              })
            .ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                    events.AddAndroid(android => android
                        .OnActivityResult((activity, requestCode, resultCode, data) => LogEvent(nameof(AndroidLifecycle.OnActivityResult), requestCode.ToString()))
                        .OnStart((activity) => LogEvent(nameof(AndroidLifecycle.OnStart)))
                        .OnCreate((activity, bundle) => LogEvent(nameof(AndroidLifecycle.OnCreate)))
                        .OnBackPressed((activity) => LogEvent(nameof(AndroidLifecycle.OnBackPressed)) && false)
                        .OnStop((activity) => LogEvent(nameof(AndroidLifecycle.OnStop))));
#endif

                static bool LogEvent(string eventName, string type = null)
                {
                    System.Diagnostics.Debug.WriteLine($"Lifecycle event: {eventName}{(type == null ? string.Empty : $" ({type})")}");
                    return true;
                }
            })
            .UseMauiCommunityToolkit()
            .UseMauiCameraView()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FARegualr");
                fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FARegualrBrand");
                fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FASolid");
            });
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        //Views
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<DashboardPage>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddSingleton<WebViewPage>();
        //View Models
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<DashboardPageViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        builder.Services.AddSingleton<WebViewViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
