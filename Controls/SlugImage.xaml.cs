namespace Slugrace.Controls;

public partial class SlugImage : ContentView
{
    public SlugImage()
    {
        InitializeComponent();

        uint rotationSpeed = (uint)new Random().Next(2000, 4000);

        new Animation()
        {
            {0, .5, new Animation(v => leftEye.Rotation = v, 0, -30) },
            {0, .5, new Animation(v => rightEye.Rotation = v, 0, 30) },
            {.5, 1, new Animation(v => leftEye.Rotation = v, -30, 0) },
            {.5, 1, new Animation(v => rightEye.Rotation = v, 30, 0) }
        }.Commit(this, "eyeRotation", 16, rotationSpeed, null, null, () => true);
    }
}
