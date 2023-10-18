namespace Slugrace.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        VisualStateManager.GoToState(maxRacesEntry, "Empty");
        VisualStateManager.GoToState(maxTimeEntry, "Empty");
    }

    private void OnMaxRacesTextChanged(object sender, TextChangedEventArgs e)
    {
        Helpers.ValidateNumericInputAndSetState(e.NewTextValue, 1, 100, maxRacesEntry);
    }

    private void OnMaxTimeTextChanged(object sender, TextChangedEventArgs e)
    {
        Helpers.ValidateNumericInputAndSetState(e.NewTextValue, 1, 120, maxTimeEntry);
    }
}