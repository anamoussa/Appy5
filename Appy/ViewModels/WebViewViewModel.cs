using Appy.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
namespace Appy.ViewModels;
public partial class WebViewViewModel : BaseViewModel, IRecipient<MyMessage>
{
    [ObservableProperty]
    public string _source;

    [ObservableProperty]
    public bool _isLoading;
    private readonly IRestServiceCall _service = new RestServiceCall();
    string result = "";
    public WebViewViewModel()
    {
        // TODO: Update the default URL
        WeakReferenceMessenger.Default.Register(this);
        Source = "https://github.com/sponsors/mrlacey";
        IsLoading = true;
    }

    private async Task<string> putDatain_webView(string search)
    {
        //  if (result is not null && result != "")

        if (String.IsNullOrEmpty(search))
            return null;
        var res = await _service.Search(search, "zzzzzzzz");
        if (res is null)
            return null;
        return res.webview.ToString();
    }

    [RelayCommand]
    private async void WebViewNavigated(WebNavigatedEventArgs e)
    {
        IsLoading = false;

        if (e.Result != WebNavigationResult.Success)
        {
            // TODO: handle failed navigation in an appropriate way
            await Shell.Current.DisplayAlert("Navigation failed", e.Result.ToString(), "OK");
        }
    }

    [RelayCommand]
    private void NavigateBack(WebView webView)
    {
        if (webView.CanGoBack)
        {
            webView.GoBack();
        }
    }

    [RelayCommand]
    private void NavigateForward(WebView webView)
    {
        if (webView.CanGoForward)
        {
            webView.GoForward();
        }
    }

    [RelayCommand]
    private void RefreshPage(WebView webView)
    {
        webView.Reload();
    }

    [RelayCommand]
    private async void OpenInBrowser()
    {
        await Launcher.OpenAsync(Source);
    }

    public void Receive(MyMessage message)
    {
        result = message.Value;

        Shell.Current.DisplayAlert("Message received", message.Value, "OK");
        var search = putDatain_webView(result);
        if(search != null)
        {
            Source = search.Result;
        }

    }
}