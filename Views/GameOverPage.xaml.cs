using Microsoft.Maui.Controls.Xaml;
using Slugrace.ViewModels;

namespace Slugrace.Views;

public partial class GameOverPage : ContentPage
{
	public GameOverPage(GameOverViewModel gameOverViewModel)
	{
		InitializeComponent();
		BindingContext = gameOverViewModel;
	}
}