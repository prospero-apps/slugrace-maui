using Slugrace.Views;

namespace Slugrace;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(RacePage), typeof(RacePage));
        Routing.RegisterRoute(nameof(GameOverPage), typeof(GameOverPage));
        Routing.RegisterRoute(nameof(InstructionsPage), typeof(InstructionsPage));
    }
}
