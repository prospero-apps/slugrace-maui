namespace Slugrace.Controls;

public partial class PlayerSettings : ContentView
{
	public PlayerSettings()
	{
		InitializeComponent();
		GoToNameState(true);
        VisualStateManager.GoToState(initialMoneyEntry, "Empty");
    }

    private void OnNameTextChanged(object sender, TextChangedEventArgs e)
    {
		bool nameValid = e.NewTextValue.Length <= 10;
		GoToNameState(nameValid);
    }

    private void OnInitialMoneyTextChanged(object sender, TextChangedEventArgs e)
    {
        Helpers.ValidateNumericInputAndSetState(e.NewTextValue, 10, 5000, initialMoneyEntry);
    }

    void GoToNameState(bool nameValid)
    {
        string visualState = nameValid ? "Valid" : "Invalid";
        VisualStateManager.GoToState(nameEntry, visualState);
    }
}