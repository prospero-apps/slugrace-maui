using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Maui.Audio;

namespace Slugrace.ViewModels;

public partial class SoundViewModel : ObservableObject
{
    private readonly IAudioManager audioManager;
    private IAudioPlayer audioPlayer;
    private List<IAudioPlayer> effectPlayers;
    private List<IAudioPlayer> loopingAccidentPlayers;

    [ObservableProperty]
    private double volume;

    [ObservableProperty]
    private bool muted;

    public SoundViewModel(IAudioManager audioManager)
    {
        this.audioManager = audioManager;
        effectPlayers = [];
        loopingAccidentPlayers = [];
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
        
    public async Task PlaySound(string folderName, string fileName, double volume = 1, bool loop = false, bool loopingAccidentSound = false)
    {
        string path = Path.Combine("Sounds", folderName, fileName);

        var player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(path));

        if (!loopingAccidentSound)
        {
            effectPlayers.Add(player);
        }
        else
        {
            loopingAccidentPlayers.Add(player);
        }
        
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
       
    public void Clean(bool loopingAccidentSound = false)
    {
        if (!loopingAccidentSound)
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
        else
        {
            foreach (var player in loopingAccidentPlayers)
            {
                if (player.IsPlaying)
                {
                    player.Stop();
                }

                player.Dispose();
            }

            loopingAccidentPlayers.Clear();
        }
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
