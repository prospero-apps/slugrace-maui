using CommunityToolkit.Maui.Views;
using Slugrace.Popups;
using Slugrace.ViewModels;

namespace Slugrace.Views;

public partial class GameOverPage : ContentPage
{
	public GameOverPage(GameOverViewModel gameOverViewModel)
	{
		InitializeComponent();
		BindingContext = gameOverViewModel;
	}

    private void QuitButtonClicked(object sender, EventArgs e)
    {
        this.ShowPopup(new QuitPopup());
    }
}