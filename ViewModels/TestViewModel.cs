using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Slugrace.ViewModels;

public class TestViewModel : INotifyPropertyChanged
{
    string favoriteColor;

    public string FavoriteColor
    {
        get => favoriteColor;
        set
        {
            if (favoriteColor != value)
            {
                favoriteColor = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LetterCount));
            }
        }
    }

    public int? LetterCount => FavoriteColor?.Length;

    public ICommand UseColorCommand { get; private set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public TestViewModel()
    {
        UseColorCommand = new Command<string>(UseFixedColor);
    }

    private void UseFixedColor(string color)
    {
        FavoriteColor = color;
    }

    void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
