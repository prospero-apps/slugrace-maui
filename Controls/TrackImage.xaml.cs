using Slugrace.Models;
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

    // accident handling
    SlugImage slugImage;
    string runningAnimationName;

    string brokenLegImage;
    string overheatBodyImage;
    string overheatEyeImage;
    string heartImage;
    string grassImage;
    string puddleImage;
    string boltImage;
    string monsterImage;

    Image accidentImage;
    Animation accidentAnimation;

    public TrackImage()
	{
		InitializeComponent();

        overheatBodyImage = "overheat_body.png";
        overheatEyeImage = "overheat_eye.png";
        heartImage = "heart_attack.png";
        grassImage = "grass.png";
        puddleImage = "puddle.png";
        boltImage = "electroshock.png";
        monsterImage = "slug_monster.png";
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
            HandleRunning();
        }
        else if (e.PropertyName == nameof(vm.AccidentShouldHappen))
        {
            HandleAccident();            
        }
    }    

    private void HandleRunning()
    {
        if (vm.RaceStatus == RaceStatus.Started)
        {
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

            if (layout.Contains(accidentImage))
            {
                if (accidentImage.ZIndex != 0)
                {
                    accidentImage.ZIndex = 0;
                }

                layout.Remove(accidentImage);
            }

            if (accidentAnimation != null)
            {
                this.AbortAnimation("accidentAnimation");                
            }

            if (slugImage != null)
            {
                slugImage.StartEyeRotation();

                if (slugImage.ZIndex != 0)
                {
                    slugImage.ZIndex = 0;
                }

                if (slugImage.Opacity != 1)
                {
                    slugImage.Opacity = 1;
                }

                if (slugImage.ScaleX != 1)
                {
                    slugImage.ScaleX = 1;
                }
            }            
        }
        else
        {
            this.AbortAnimation("moveSpeedster");
            this.AbortAnimation("moveTrusty");
            this.AbortAnimation("moveIffy");
            this.AbortAnimation("moveSlowpoke");
        }
    }

    private async Task HandleAccident()
    {
        if (vm.AccidentShouldHappen)
        {            
            // slug data
            if (vm.AccidentViewModel.AffectedSlug == vm.Slugs[0])
            {
                slugImage = speedster;
                runningAnimationName = "moveSpeedster";
                brokenLegImage = "broken_leg_speedster.png";
            }
            else if (vm.AccidentViewModel.AffectedSlug == vm.Slugs[1])
            {
                slugImage = trusty;
                runningAnimationName = "moveTrusty";
                brokenLegImage = "broken_leg_trusty.png";
            }
            else if (vm.AccidentViewModel.AffectedSlug == vm.Slugs[2])
            {
                slugImage = iffy;
                runningAnimationName = "moveIffy";
                brokenLegImage = "broken_leg_iffy.png";
            }
            else if (vm.AccidentViewModel.AffectedSlug == vm.Slugs[3])
            {
                slugImage = slowpoke;
                runningAnimationName = "moveSlowpoke";
                brokenLegImage = "broken_leg_slowpoke.png";
            }

            
            // accident type
            switch (vm.AccidentViewModel.AccidentType)
            {
                case AccidentType.BrokenLeg:
                    await HandleBrokenLeg();
                    break;
                case AccidentType.Overheat:
                    await HandleOverheat();
                    break;
                case AccidentType.HeartAttack:
                    await HandleHeartAttack();
                    break;
                case AccidentType.Grass:
                    await HandleGrass();
                    break;
                case AccidentType.Asleep:
                    await HandleAsleep();
                    break;
                case AccidentType.Blind:
                    await HandleBlind();
                    break;
                case AccidentType.Puddle:
                    await HandlePuddle();
                    break;
                case AccidentType.Electroshock:
                    await HandleElectroshock();
                    break;
                case AccidentType.TurningBack:
                    await HandleTurningBack();
                    break;
                case AccidentType.Devoured:
                    await HandleDevoured();
                    break;
                default:
                    break;
            }
        }
    }

    private async Task HandleBrokenLeg()
    {
        await Task.Delay((int)vm.AccidentViewModel.TimePosition);
        this.AbortAnimation(runningAnimationName);
        vm.AccidentViewModel.AffectedSlug.BodyImageUrl = brokenLegImage;
        vm.PlayAccidentSound();
        vm.DisplayAccidentPopup();
    }

    private async Task HandleOverheat()
    {
        await Task.Delay((int)vm.AccidentViewModel.TimePosition);
        this.AbortAnimation(runningAnimationName);
        slugImage.StopEyeRotation();
        vm.AccidentViewModel.AffectedSlug.BodyImageUrl = overheatBodyImage;
        vm.AccidentViewModel.AffectedSlug.EyeImageUrl = overheatEyeImage;
        vm.PlayAccidentSound();
        vm.DisplayAccidentPopup();
    }
       
    private async Task HandleHeartAttack()
    {
        await Task.Delay((int)vm.AccidentViewModel.TimePosition);
        this.AbortAnimation(runningAnimationName);

        accidentImage = new Image { Source = heartImage };
        layout.Add(accidentImage);

#if ANDROID                
        layout.SetLayoutBounds(accidentImage, new Rect(
            slugImage.X + slugImage.TranslationX - .2 * slugImage.Width,
            slugImage.Y - slugImage.Height,
            accidentImage.Width,
            accidentImage.Height));

        accidentAnimation = new Animation()
        {
            {0, .24, new Animation(v => accidentImage.Scale = v, .4, .3) },
            {.24, 1, new Animation(v => accidentImage.Scale = v, .3, .4) }
        };
#endif

#if WINDOWS
        layout.SetLayoutBounds(accidentImage, new Rect(
            slugImage.X + slugImage.TranslationX + .4 * slugImage.Width,
            slugImage.Y,
            accidentImage.Width,
            accidentImage.Height));

        accidentAnimation = new Animation()
        {
            {0, .24, new Animation(v => accidentImage.Scale = v, .8, .6) },
            {.24, 1, new Animation(v => accidentImage.Scale = v, .6, .8) }
        };
#endif

        accidentAnimation.Commit(this, "accidentAnimation", 16, 820, Easing.CubicInOut, null, () => true);

        vm.PlayAccidentSound(true, true);
        vm.DisplayAccidentPopup();
    }

    private async Task HandleGrass()
    {
        await Task.Delay((int)vm.AccidentViewModel.TimePosition);
        this.AbortAnimation(runningAnimationName);

        accidentImage = new Image { Source = grassImage };
        layout.Add(accidentImage);

#if ANDROID
        accidentImage.ScaleX = .5;
        accidentImage.ScaleY = .25;

        layout.SetLayoutBounds(accidentImage, new Rect(
                slugImage.X + slugImage.TranslationX + .5 * slugImage.Width,
                slugImage.Y - 2 * slugImage.Height,
                accidentImage.Width,
                accidentImage.Height));    
#endif

#if WINDOWS
        layout.SetLayoutBounds(accidentImage, new Rect(
                slugImage.X + slugImage.TranslationX + slugImage.Width,
                .9 * slugImage.Y,
                accidentImage.Width,
                accidentImage.Height));
#endif       

        accidentAnimation = new Animation()
        {
            {0, .24, new Animation(v => slugImage.ScaleX = v, 1, .9) },
            {.24, 1, new Animation(v => slugImage.ScaleX = v, .9, 1) }
        };

        accidentAnimation.Commit(this, "accidentAnimation", 16, 500, Easing.CubicInOut, null, () => true);

        vm.PlayAccidentSound(true, true);
        vm.DisplayAccidentPopup();

        await Task.Delay((int)vm.AccidentViewModel.Duration);

        this.AbortAnimation("accidentAnimation");

        if (layout.Contains(accidentImage))
        {
            layout.Remove(accidentImage);
        }

        vm.StopAccidentSound();
                
        accidentAnimation = new Animation(v => slugImage.TranslationX = v, slugImage.X + slugImage.TranslationX, trackLength);
        accidentAnimation.Commit(this, "accidentAnimation", 16, vm.AccidentViewModel.AffectedSlug.RunningTime / 4, Easing.Linear, null, () => false);
    }

    private async Task HandleAsleep()
    {
        await Task.Delay((int)vm.AccidentViewModel.TimePosition);
        this.AbortAnimation(runningAnimationName);

        slugImage.StopEyeRotation();

        accidentAnimation = new Animation()
        {
            {0, .46, new Animation(v => slugImage.Scale = v, 1, 1.05) },
            {.46, 1, new Animation(v => slugImage.Scale = v, 1.05, 1) }
        };

        accidentAnimation.Commit(this, "accidentAnimation", 16, 5600, Easing.CubicInOut, null, () => true);

        vm.PlayAccidentSound(true, true);
        vm.DisplayAccidentPopup();
    }

    private async Task HandleBlind()
    {
        await Task.Delay((int)vm.AccidentViewModel.TimePosition);

        vm.AccidentViewModel.AffectedSlug.EyeImageUrl = null;

        accidentAnimation = new Animation()
        {
            {0, .25, new Animation(v => slugImage.Rotation = v, 0, 15) },
            {.25, .5, new Animation(v => slugImage.Rotation = v, 15, 0) },
            {.5, .75, new Animation(v => slugImage.Rotation = v, 0, -15) },
            {.75, 1, new Animation(v => slugImage.Rotation = v, -15, 0) }
        };

        accidentAnimation.Commit(this, "accidentAnimation", 16, 1000, Easing.Linear, null, () => true);                    

        vm.PlayAccidentSound();
        vm.DisplayAccidentPopup();

        await Task.Delay((int)vm.AccidentViewModel.Duration);

        slugImage.Rotation = 0;

        this.AbortAnimation("accidentAnimation");
    }
        
    private async Task HandlePuddle()
    {
        await Task.Delay((int)vm.AccidentViewModel.TimePosition);
        this.AbortAnimation(runningAnimationName);

        accidentImage = new Image { Source = puddleImage };
        accidentImage.ZIndex = 1;
        slugImage.ZIndex = 2;

        layout.Add(accidentImage);

#if ANDROID
        accidentImage.ScaleX = .25;
        accidentImage.ScaleY = .25;

        layout.SetLayoutBounds(accidentImage, new Rect(
        slugImage.X + slugImage.TranslationX - 3 * slugImage.Width,
        slugImage.Y - 2 * slugImage.Height,
        accidentImage.Width,
        accidentImage.Height));               
#endif

#if WINDOWS
        layout.SetLayoutBounds(accidentImage, new Rect(
        slugImage.X + slugImage.TranslationX - slugImage.Width / 3,
        slugImage.Y - .3 * slugImage.Height,
        accidentImage.Width,
        accidentImage.Height));
#endif

        accidentAnimation = new Animation()
        {
            {0, .1, new Animation(v => slugImage.Opacity = v, .6, 0) },
            {.1, .8, new Animation(v => slugImage.Opacity = v, 0, .3) },
            {.8, 1, new Animation(v => slugImage.Opacity = v, .3, .6) }
        };

        accidentAnimation.Commit(this, "accidentAnimation", 16, vm.RaceTime, Easing.CubicInOut, null, () => true);
                
        vm.PlayAccidentSound(true, true);
        vm.DisplayAccidentPopup();
    }

    private async Task HandleElectroshock()
    {
        await Task.Delay((int)vm.AccidentViewModel.TimePosition);
        this.AbortAnimation(runningAnimationName);

        accidentImage = new Image { Source = boltImage };
        layout.Add(accidentImage);

        accidentImage.Rotation = -15;

#if ANDROID
        accidentImage.Scale = .2;

        layout.SetLayoutBounds(accidentImage, new Rect(
                slugImage.X + slugImage.TranslationX - slugImage.Width,
                slugImage.Y - 2 * slugImage.Height,
                accidentImage.Width,
                accidentImage.Height));    
#endif

#if WINDOWS
        accidentImage.Scale = .7;

        layout.SetLayoutBounds(accidentImage, new Rect(
                slugImage.X + slugImage.TranslationX,
                .9 * slugImage.Y,
                accidentImage.Width,
                accidentImage.Height));
#endif

        accidentAnimation = new Animation()
        {
            {0, .5, new Animation(v => accidentImage.Opacity = v, 0, 1) },
            {.5, 1, new Animation(v => accidentImage.Opacity = v, 1, 0) }
        };

        accidentAnimation.Commit(this, "accidentAnimation", 16, 500, Easing.Linear, null, () => true);

        vm.PlayAccidentSound(true, true);
        vm.DisplayAccidentPopup();

        await Task.Delay((int)vm.AccidentViewModel.Duration);

        this.AbortAnimation("accidentAnimation");

        if (layout.Contains(accidentImage))
        {
            layout.Remove(accidentImage);
        }

        vm.StopAccidentSound();

        accidentAnimation = new Animation(v => slugImage.TranslationX = v, slugImage.TranslationX, trackLength);
        accidentAnimation.Commit(this, "accidentAnimation", 16, vm.AccidentViewModel.AffectedSlug.RunningTime / 4, Easing.Linear, null, () => false);

    }

    private async Task HandleTurningBack()
    {
        await Task.Delay((int)vm.AccidentViewModel.TimePosition);
        this.AbortAnimation(runningAnimationName);
                      
        accidentAnimation = new Animation()
        {
            {0, .2, new Animation(v => slugImage.ScaleX = v, 1, -1) },
            {.2, 1, new Animation(v => slugImage.TranslationX = v, slugImage.TranslationX, -400) }
        };

        accidentAnimation.Commit(this, "accidentAnimation", 16, 5000, Easing.Linear, null, () => false);

        vm.PlayAccidentSound();
        vm.DisplayAccidentPopup();        
    }
        
    private async Task HandleDevoured()
    {
        await Task.Delay((int)vm.AccidentViewModel.TimePosition);
        this.AbortAnimation(runningAnimationName);

        accidentImage = new Image { Source = monsterImage };
        layout.Add(accidentImage);

#if ANDROID
        accidentImage.ScaleX = .25;
        accidentImage.ScaleY = .25;

        layout.SetLayoutBounds(accidentImage, new Rect(
            -100,
            slugImage.Y - 2 * slugImage.Height,
            accidentImage.Width,
            accidentImage.Height));   
            
        accidentAnimation = new Animation(v => accidentImage.TranslationX = v, 0, slugImage.TranslationX);
#endif

#if WINDOWS
        layout.SetLayoutBounds(accidentImage, new Rect(
            -accidentImage.Width,
            slugImage.Y - .3 * slugImage.Height,
            accidentImage.Width,
            accidentImage.Height));

        accidentAnimation = new Animation(v => accidentImage.TranslationX = v, 0, slugImage.X + slugImage.TranslationX + accidentImage.Width);
#endif
        
        accidentAnimation.Commit(this, "accidentAnimation", 16, 1000, Easing.CubicInOut, null, () => false);


        vm.PlayAccidentSound();
        vm.DisplayAccidentPopup();
    }
}
