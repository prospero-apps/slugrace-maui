using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Slugrace.ViewModels;

public partial class TestViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LetterCount))]    
    string favoriteColor;       

    public int? LetterCount => FavoriteColor?.Length;    

    [RelayCommand]
    void UseFixedColor(string color)
    {
        FavoriteColor = color;
    }
}
