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
        Helpers.ValidateNumericInputAndSetState(e.NewTextValue, 1, 1000, betAmountEntry);
    }
}