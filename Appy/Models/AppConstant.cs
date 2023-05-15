using Appy.Controls;
using Appy.Handlers;
using Appy.Views.Dashboard;

namespace Appy.Models;

public class AppConstant
{

    public async static Task AddFlyoutMenusDetails()
    {
        AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
        var adminDashboardInfo = AppShell.Current.Items.Where(f => f.Route == nameof(DashboardPage)).FirstOrDefault();
        if (adminDashboardInfo != null) AppShell.Current.Items.Remove(adminDashboardInfo);

            var flyoutItem = new FlyoutItem()
            {
                Title = "Dashboard",
                Route = nameof(DashboardPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                {
                            new ShellContent
                            {
                                Icon = Icons.Profile,
                                Title = "Profile",
                                ContentTemplate = new DataTemplate(typeof(DashboardPage)),
                            },
               }
            };

            if (!AppShell.Current.Items.Contains(flyoutItem))
            {
                AppShell.Current.Items.Add(flyoutItem);
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    AppShell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
                }
            }
    }
}
