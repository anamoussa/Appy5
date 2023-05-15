using Appy.Models;

namespace Appy.Controls;

public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();

        if (App.UserDetails != null)
        {
			lblUserName.Text = App.UserDetails.Username;
            //lblUserEmail.Text = App.UserDetails.Email;
            //lblUserRole.Text = App.UserDetails.Role;
            imgAvatar.ImageSource = App.UserDetails.Avatar??Icons.Profile;
        }
    }
}