
namespace Slugrace;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new AppShell();
    }

#if WINDOWS
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        window.Width = window.MinimumWidth = window.MaximumWidth = 1440;
        window.Height = window.MinimumHeight = window.MaximumHeight = 800;

        return window;
    }
#endif
}
