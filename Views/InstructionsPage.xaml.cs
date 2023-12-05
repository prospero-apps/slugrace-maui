using Slugrace.ViewModels;

namespace Slugrace.Views;

public partial class InstructionsPage : ContentPage
{
	public InstructionsPage(GameViewModel gameViewModel)
	{
		InitializeComponent();
		BindingContext = gameViewModel;
	}
}

