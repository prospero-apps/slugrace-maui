namespace Slugrace.Controls;

public partial class SlugImage : ContentView
{
    Animation eyeAnimation;
    uint rotationSpeed;

    public Image LeftEye { get; set; }
    public Image RightEye { get; set; }

    public SlugImage()
    {
        InitializeComponent();
  
        eyeAnimation = new Animation()
        {
            {0, .5, new Animation(v => leftEye.Rotation = v, 0, -30) },
            {0, .5, new Animation(v => rightEye.Rotation = v, 0, 30) },
            {.5, 1, new Animation(v => leftEye.Rotation = v, -30, 0) },
            {.5, 1, new Animation(v => rightEye.Rotation = v, 30, 0) }
        };

        LeftEye = leftEye;
        RightEye = rightEye;


        StartEyeRotation();
    }    

    public void StartEyeRotation()
    {
        rotationSpeed = (uint)new Random().Next(2000, 4000);
        eyeAnimation.Commit(this, "eyeRotation", 16, rotationSpeed, null, null, () => true);
    }
    public void StopEyeRotation()
    {
        this.AbortAnimation("eyeRotation");
    }
}
