
using Appy.Services;
using Appy.ViewModels;
using Appy.ViewModels.Dashboard;
using BarcodeScanner.Mobile;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.PlatformConfiguration;
//using ObjCRuntime;
using System.Globalization;
//using ZXing;
namespace Appy.Views.Dashboard;

public partial class DashboardPage : ContentPage
{
    string result = "";
    private readonly IRestServiceCall _service = new RestServiceCall();

    public DashboardPage(DashboardPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        WeakReferenceMessenger.Default.Register<OpenWindowMessage>(this, HandleOpenWindowMessage);
        //get current language
        Camera.IsEnabled = true;
        var xx = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        BarcodeScanner.Mobile.Methods.AskForRequiredPermission();
    }
 
 
    private void Camera_BarcodeDetected(object sender, BarcodeScanner.Mobile.OnDetectedEventArg args)
    {

        List<BarcodeResult> obj = args.BarcodeResults;

        for (int i = 0; i < obj.Count; i++)
        {
            // result += $"Type : {obj[i].BarcodeType}, Value : {obj[i].DisplayValue}{Environment.NewLine}";
            result += $"{obj[i].DisplayValue}{Environment.NewLine}";
        }
        Vibration.Vibrate(50);
        btn_scan.IsVisible = true;
        btn_cancel.IsVisible = false;
        WeakReferenceMessenger.Default.Send(new MyMessage(result));
        MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync($"//{nameof(WebViewPage)}");
            });

        Camera.IsScanning = false;

        //var toast = Toast.Make("code detected");
        // await toast.Show();
    }

    private async void btn_scan_Clicked(object sender, EventArgs e)
    {

        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        if (accessType != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Uh Oh!", "No Internet!", "Ok");
        }
        else
        {
            Camera.IsScanning = true;
            Camera.IsEnabled = true;
            Camera.IsVisible = true;
            Camera.HeightRequest = 300; Camera.WidthRequest = 300;
            imgQr.IsVisible = false;
            btn_cancel.IsVisible = true;
            btn_scan.IsVisible = false;
        }
        // await DisplayAlert("  ", $"result= {res} ", "ok");
    }
    private void btn_cancel_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(result))
        {
            //webView.IsVisible = true;
            //webView.HeightRequest = 600; //webView. WidthRequest= 370;
            imgQr.IsVisible = false;

        }
        else
        {
            imgQr.IsVisible = true;
            imgQr.Aspect = Aspect.Fill;
        }
        Camera.IsEnabled = false;
        Camera.HeightRequest = 0; Camera.WidthRequest = 0;
        Camera.IsVisible = false;
        //  Camera.TorchEnabled = false;
        btn_scan.IsVisible = true;
        FlashlightSwitch.IsVisible = false;
        btn_cancel.IsVisible = false;
        FlashlightSwitch.IsToggled = false;

    }

   

    private async void FlashlightSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        try
        {

            if (FlashlightSwitch.IsToggled)
            {
                await Flashlight.Default.TurnOnAsync();
                // Camera.TorchEnabled = true;
            }
            else
                await Flashlight.Default.TurnOffAsync();

            // Camera.TorchEnabled = false;
        }
        catch (FeatureNotSupportedException)
        {
            // await Shell.Current.DisplayAlert("Erorr", " not supported on device exception", "OK");

            // Handle not supported on device exception
        }
        catch (PermissionException)
        {
            // await Shell.Current.DisplayAlert("Erorr", " permission exception", "OK");
            // Handle permission exception
        }
        catch (Exception ex)
        {
            var x = ex.ToString();
            await Shell.Current.DisplayAlert("Erorr", $"{x} Unable to turn on/off flashlight ", "OK");
            // Unable to turn on/off flashlight
        }
    }


    protected override void OnDisappearing()
    {
        Camera.IsEnabled = false;
        //  Camera.light = false;
        FlashlightSwitch.IsToggled = false;
        FlashlightSwitch.IsVisible = false;
        btn_cancel.IsVisible = false;
        //webView.HeightRequest = 0; //webView.WidthRequest = 0;
        //webView.Source = "";
        imgQr.IsVisible = true;
        //   base.OnDisappearing();
    }
    protected override void OnAppearing()
    {
        Camera.IsEnabled = true;
        btn_scan.IsVisible = true;
        Camera.IsVisible = true;
    }

    private void HandleOpenWindowMessage(object recipient,
       OpenWindowMessage message)
    {
        if (message.Value)
        {
            Camera.HeightRequest = 1; Camera.WidthRequest = 1;
            Camera.IsEnabled = true;
            btn_scan.IsVisible = true;
            Camera.IsVisible = true;

        
        }
        else
        {
            FlashlightSwitch.IsVisible = false;
            btn_cancel.IsVisible = false;
        }

    }

}