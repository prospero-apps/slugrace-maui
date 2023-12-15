using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Maui.Audio;

namespace Slugrace.ViewModels;

public partial class SoundViewModel : ObservableObject
{
    private readonly IAudioManager audioManager;
    private IAudioPlayer audioPlayer;
    private List<IAudioPlayer> effectPlayers;

    [ObservableProperty]
    private double volume;

    [ObservableProperty]
    private bool muted;

    public SoundViewModel(IAudioManager audioManager)
    {
        this.audioManager = audioManager;
        effectPlayers = [];
        Volume = .3;
    }    

    public async Task PlayBackgroundMusic(string folderName, string fileName)
    {
        string path = Path.Combine("Sounds", folderName, fileName);

        audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(path));
        audioPlayer.Loop = true;
        audioPlayer.Volume = Volume;
        audioPlayer.Play();
    }

    public async Task PlaySound(string folderName, string fileName, double volume = 1, bool loop = false)
    {
        Clean();

        string path = Path.Combine("Sounds", folderName, fileName);

        var player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(path));

        effectPlayers.Add(player);

        player.Volume = volume;
        player.Loop = loop;
        player.Play();
    }

    public void Stop()
    {
        if (audioPlayer.IsPlaying)
        {
            audioPlayer.Stop();
            audioPlayer.Dispose();
        }        
    }

    public void Clean()
    {
        foreach (var player in effectPlayers)
        {
            if (player.IsPlaying)
            {
                player.Stop();
            }

            player.Dispose();
        }

        effectPlayers.Clear();
    }

    public async Task Attenuate()
    {
        while (audioPlayer.Volume > 0.01)
        {
            await Task.Delay(10);
            audioPlayer.Volume -= .001;
        }
    }

    public void MuteUnmute()
    {
        double defaultVolume = Volume > 0 ? Volume : .3;

        Muted = !Muted;

        Volume = Muted ? 0 : defaultVolume;

        audioPlayer.Volume = Volume;
    }
}
