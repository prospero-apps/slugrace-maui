using Microsoft.Extensions.Logging;
using Slugrace.Views;
using Slugrace.ViewModels;
using CommunityToolkit.Maui;
using Plugin.Maui.Audio;

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
		builder.Services.AddTransient<TestPage>();
		builder.Services.AddTransient<TestViewModel>();

		builder.Services.AddTransient<SettingsPage>();
		builder.Services.AddTransient<SettingsViewModel>();

		builder.Services.AddTransient<RacePage>();
        builder.Services.AddTransient<GameViewModel>();

		builder.Services.AddTransient<GameOverPage>();
		builder.Services.AddTransient<GameOverViewModel>();

        builder.Services.AddTransient<InstructionsPage>();

		builder.Services.AddTransient<SoundViewModel>();

		builder.Services.AddSingleton(AudioManager.Current);

        return builder.Build();
	}
}
