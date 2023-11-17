using Slugrace.ViewModels;

namespace Slugrace.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel settingsViewModel)
    {
        InitializeComponent();
        BindingContext = settingsViewModel; 
    }
    
    private void OnMaxRacesTextChanged(object sender, TextChangedEventArgs e)
    {        
        if (BindingContext != null && maxRacesEntry != null)
        {
            bool maxRacesValid = (BindingContext as SettingsViewModel).MaxRacesIsValid;

            Helpers.HandleNumericEntryState(maxRacesValid, maxRacesEntry);
        }
    }

    private void OnMaxTimeTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext != null && maxTimeEntry != null)
        {
            bool maxTimeValid = (BindingContext as SettingsViewModel).MaxTimeIsValid;

            Helpers.HandleNumericEntryState(maxTimeValid, maxTimeEntry);
        }
    }
}