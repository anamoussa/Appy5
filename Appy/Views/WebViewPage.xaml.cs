using Appy.ViewModels;
using Appy.Views.Dashboard;
using CommunityToolkit.Mvvm.Messaging;

namespace Appy.Views;

public partial class WebViewPage : ContentPage
{
    public WebViewPage(WebViewViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");

    }
}