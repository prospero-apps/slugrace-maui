using Slugrace.ViewModels;

namespace Slugrace.Controls;

public partial class PlayerSettings : ContentView
{
    public PlayerSettings()
    {
        InitializeComponent();
    }

    private void OnNameTextChanged(object sender, TextChangedEventArgs e)
    {
        bool nameValid = (BindingContext as PlayerSettingsViewModel).NameIsValid;
        GoToNameState(nameValid);
    }

    private void OnInitialMoneyTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext != null)
        {
            bool initialMoneyValid = (BindingContext as PlayerSettingsViewModel).InitialMoneyIsValid;
            Helpers.HandleNumericEntryState(initialMoneyValid, initialMoneyEntry);
        }
    }

    void GoToNameState(bool nameValid)
    {
        string visualState = nameValid ? "Valid" : "Invalid";
        VisualStateManager.GoToState(nameEntry, visualState);
    }
}

