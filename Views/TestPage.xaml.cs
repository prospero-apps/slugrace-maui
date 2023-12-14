using Slugrace.ViewModels;

namespace Slugrace.Views;
public partial class TestPage : ContentPage
{
    TestViewModel vm;

    readonly Animation rotation;

    public TestPage(TestViewModel testViewModel)
    {
        InitializeComponent();     

        rotation = new Animation(HandleTransformations, 0, 360);

        BindingContext = testViewModel;
        vm = (TestViewModel)BindingContext;

        vm.PropertyChanged += Vm_PropertyChanged;
    }

    void HandleTransformations(double value)
    {
        box.Rotation = value;

        label.Text = ((int)value).ToString();

        if ((int)value % 20 == 0)
        {
            if (vm.IsHidden)
            {
                ShowBox();
            }
            else
            {
                HideBox();
            }           
        }        
    }

    void HideBox()
    {
        box.Opacity = 0;
        vm.IsHidden = true;
    }

    void ShowBox()
    {
        box.Opacity = 1;
        vm.IsHidden = false;
    }

    private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(vm.IsRunning))
        {
            if (vm.IsRunning)
            {
                rotation.Commit(this, "rotate", 16, 5000, Easing.Linear,
                    (v, c) => box.Rotation = 0, () => false);                               
            }
            else
            {
                this.AbortAnimation("rotate");
            }
        }
    }
}
