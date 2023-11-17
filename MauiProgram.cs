using Microsoft.Extensions.Logging;
using Slugrace.Views;
using Slugrace.ViewModels;
using CommunityToolkit.Maui;

namespace Slugrace;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<GameManager>();

		builder.Services.AddSingleton<TestPage>();
		builder.Services.AddSingleton<TestViewModel>();

		builder.Services.AddSingleton<SettingsPage>();
		builder.Services.AddSingleton<SettingsViewModel>();

        return builder.Build();
	}
}
