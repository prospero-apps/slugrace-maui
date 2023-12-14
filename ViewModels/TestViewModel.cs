using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Slugrace.ViewModels;

public partial class TestViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isHidden;

    [ObservableProperty]
    private bool isRunning;

    [RelayCommand]
    async Task Animate()
    {
        IsRunning = true;
        await Task.Delay(5000);
        IsRunning = false;
    }
}
