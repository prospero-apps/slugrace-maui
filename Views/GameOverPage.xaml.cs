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