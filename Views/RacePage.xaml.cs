using Slugrace.ViewModels;

namespace Slugrace.Views;

public partial class RacePage : ContentPage
{
    public RacePage(GameViewModel gameViewModel)
    {
        InitializeComponent();
        BindingContext = gameViewModel;
    }
}