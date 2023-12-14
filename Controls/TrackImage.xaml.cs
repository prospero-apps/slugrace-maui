using Slugrace.ViewModels;
using System.ComponentModel;

namespace Slugrace.Controls;

public partial class TrackImage : ContentView
{
	GameViewModel vm;
    double trackLength;

    Animation speedsterMovement;
    Animation trustyMovement;
    Animation iffyMovement;
    Animation slowpokeMovement;

    public TrackImage()
	{
		InitializeComponent();
    }

    protected override void LayoutChildren(double x, double y, double width, double height)
    {
        base.LayoutChildren(x, y, width, height);
        trackLength = track.Width * .79;

        speedsterMovement = new Animation(SpeedsterMoveForward, 0, trackLength);
        trustyMovement = new Animation(TrustyMoveForward, 0, trackLength);
        iffyMovement = new Animation(IffyMoveForward, 0, trackLength);
        slowpokeMovement = new Animation(SlowpokeMoveForward, 0, trackLength);
    }

    private void ContentView_BindingContextChanged(object sender, EventArgs e)
    {
        vm = (GameViewModel)BindingContext;
        vm.PropertyChanged += Vm_PropertyChanged;
    }
        
    private void SpeedsterMoveForward(double value)
    {
        speedster.TranslationX = value;
    }
        

    private void TrustyMoveForward(double value)
    {
        trusty.TranslationX = value;
    }

    private void IffyMoveForward(double value)
    {
        iffy.TranslationX = value;
    }

    private void SlowpokeMoveForward(double value)
    {
        slowpoke.TranslationX = value;
    }

    private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(vm.RaceStatus))
        {
            if (vm.RaceStatus == RaceStatus.Started)
            {
                vm.Slugs[0].RunningTime = (uint)new Random().Next(3000, 7000);
                vm.Slugs[1].RunningTime = (uint)new Random().Next(4000, 7000);
                vm.Slugs[2].RunningTime = (uint)new Random().Next(4000, 6000);
                vm.Slugs[3].RunningTime = (uint)new Random().Next(5000, 8000);
                             
                uint[] runningTimes = [
                    vm.Slugs[0].RunningTime,
                    vm.Slugs[1].RunningTime,
                    vm.Slugs[2].RunningTime,
                    vm.Slugs[3].RunningTime
                ];

                vm.RaceTime = runningTimes.Max();
                vm.MinTime = runningTimes.Min();
                vm.FinishTime = (uint)(.79 * vm.MinTime);

                speedsterMovement.Commit(this, "moveSpeedster", 16, vm.Slugs[0].RunningTime, Easing.Linear,
                    null, () => false);
                trustyMovement.Commit(this, "moveTrusty", 16, vm.Slugs[1].RunningTime, Easing.Linear,
                    null, () => false);
                iffyMovement.Commit(this, "moveIffy", 16, vm.Slugs[2].RunningTime, Easing.Linear,
                    null, () => false);
                slowpokeMovement.Commit(this, "moveSlowpoke", 16, vm.Slugs[3].RunningTime, Easing.Linear,
                    null, () => false);               
            }
            else if (vm.RaceStatus == RaceStatus.NotYetStarted)
            {
                speedster.TranslationX = 0;
                trusty.TranslationX = 0;
                iffy.TranslationX = 0;
                slowpoke.TranslationX = 0;
            }
            else
            {
                this.AbortAnimation("moveSpeedster");
                this.AbortAnimation("moveTrusty");
                this.AbortAnimation("moveIffy");
                this.AbortAnimation("moveSlowpoke");
            }
        }
    }
}