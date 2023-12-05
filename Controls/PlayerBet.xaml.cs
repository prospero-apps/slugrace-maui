using Slugrace.ViewModels;

namespace Slugrace.Controls;

public partial class PlayerBet : ContentView
{
	public PlayerBet()
	{
		InitializeComponent();
        VisualStateManager.GoToState(betAmountEntry, "Empty");
    }

    private void OnBetAmountTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext != null)
        {
            bool betAmountValid = (BindingContext as PlayerViewModel).BetAmountIsValid;
            Helpers.HandleNumericEntryState(betAmountValid, betAmountEntry);
        }
    }
}