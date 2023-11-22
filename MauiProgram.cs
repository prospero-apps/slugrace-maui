using Microsoft.Extensions.Logging;
using Slugrace.Views;
using Slugrace.ViewModels;
using CommunityToolkit.Maui;
using Slugrace.Controls;

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

		builder.Services.AddTransient<TestPage>();
		builder.Services.AddTransient<TestViewModel>();

		builder.Services.AddTransient<SettingsPage>();
		builder.Services.AddTransient<SettingsViewModel>();

        builder.Services.AddTransient<RacePage>();
        builder.Services.AddTransient<RaceViewModel>();

        builder.Services.AddTransient<GameOverPage>();
        builder.Services.AddTransient<GameOverViewModel>();		       

        return builder.Build();
	}
}
