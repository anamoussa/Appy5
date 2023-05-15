using Newtonsoft.Json;
using Appy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Appy.Services;

namespace Appy.ViewModels.Startup
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        private readonly IRestServiceCall _service = new RestServiceCall();
        private readonly IConnectivity _connectivity;

        public LoginPageViewModel(IConnectivity connectivity)
        {
            _connectivity = connectivity;
        }
        #region Commands
        [CommunityToolkit.Mvvm.Input.RelayCommand]
        async Task Login()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet!", "Ok");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
                {
                    var userDetails = new UserBasicInfo();
                    //userDetails.Username = UserName;
                    //userDetails.Username = "Test User Name";

                    // calling api
                  //  var result = await _service.Login("M_rh50@yahoo.com", "M_rh50@yahoo.com", "zzzzzzzz");
                    var result = await _service.Login(UserName, Password,"");

                    if(result.Id==0||result==null)
                    {
                        await Shell.Current.DisplayAlert("Opps", "Incorrect user Name or Passord", "Retry");
                        return;
                    }
                    userDetails = result;
                    if (Preferences.ContainsKey(nameof(App.UserDetails)))
                    {
                        Preferences.Remove(nameof(App.UserDetails));
                    }

                    string userDetailStr = JsonConvert.SerializeObject(userDetails);
                    Preferences.Set(nameof(App.UserDetails), userDetailStr);
                    App.UserDetails = userDetails;
                    await AppConstant.AddFlyoutMenusDetails();
                }
            }
        }
        #endregion
    }
}
