using Slugrace.Views;

namespace Slugrace;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(RacePage), typeof(RacePage));
	}
}
