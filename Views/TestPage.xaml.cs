using Slugrace.ViewModels;

namespace Slugrace.Views;
public partial class TestPage : ContentPage
{
    public TestPage(TestViewModel testViewModel)
    {
        InitializeComponent();
        BindingContext = testViewModel;
    }
}
