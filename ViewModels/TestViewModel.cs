using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Slugrace.ViewModels;

[QueryProperty(nameof(Team), "Team")]
public partial class TestViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<PlayerSettingsViewModel> team;

    [RelayCommand]
    async Task NavigateBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
