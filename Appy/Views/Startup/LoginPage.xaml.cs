using Appy.ViewModels.Startup;

namespace Appy.Views.Startup;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (nameValidation.IsNotValid)
        {
            await DisplayAlert("Error", "User Name is not in correct syntax", "Retry");
            return;
        }
        if (passValidation.IsNotValid)
        {
            await DisplayAlert("Error", "Password is not in correct syntax", "Retry");
            return;

        }
        if (nameValidation.IsValid && passValidation.IsValid)
        {

            var viewModel = BindingContext as LoginPageViewModel;
            if (viewModel.LoginCommand.CanExecute(null))
            {
                signin_btn.IsInProgress = true;
                await Task.Delay(500);//for test

                viewModel.LoginCommand.Execute(null);

                signin_btn.IsInProgress = false;

            }
        }



    }
}