using CommunityToolkit.Maui.Views;
using Slugrace.ViewModels;

namespace Slugrace.Popups;

public partial class AccidentPopup : Popup
{
	public AccidentPopup(AccidentPopupViewModel accidentPopupViewModel)
	{
		InitializeComponent();
        BindingContext = accidentPopupViewModel;
    }
}