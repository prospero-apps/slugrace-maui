using Slugrace.ViewModels;

namespace Slugrace.Views;

public partial class RacePage : ContentPage
{
	public RacePage(RaceViewModel raceViewModel)
	{
		InitializeComponent();
		BindingContext = raceViewModel;
    }
}