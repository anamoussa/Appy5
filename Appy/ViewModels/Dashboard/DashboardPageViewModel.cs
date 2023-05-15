using Appy.Controls;

namespace Appy.ViewModels.Dashboard;

public partial class DashboardPageViewModel : BaseViewModel
{
    public DashboardPageViewModel()
    {
        AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
    }
}
