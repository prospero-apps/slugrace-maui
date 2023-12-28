using CommunityToolkit.Maui.Views;

namespace Slugrace.Popups;

public partial class QuitPopup : Popup
{
	public QuitPopup()
	{
		InitializeComponent();
    }

    private async void CancelButtonClicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }

    private void QuitButtonClicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}