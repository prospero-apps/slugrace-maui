using CommunityToolkit.Mvvm.ComponentModel;
using Slugrace.Models;

namespace Slugrace.ViewModels;

public partial class AccidentViewModel : ObservableObject
{
    private Accident accident;

    private readonly Dictionary<AccidentType, string> AccidentNames = new()
    {
        { AccidentType.BrokenLeg, "Broken Leg" },
        { AccidentType.Overheat, "Overheat" },
        { AccidentType.HeartAttack, "Heart Attack" },
        { AccidentType.Grass, "Grass" },
        { AccidentType.Asleep, "Asleep" },
        { AccidentType.Blind, "Blind" },
        { AccidentType.Puddle, "Puddle" },
        { AccidentType.Electroshock, "Electroshock" },
        { AccidentType.TurningBack, "Turning Back" },
        { AccidentType.Devoured, "Devoured" }
    };

    private readonly Dictionary<AccidentType, string[]> Headlines = new()
    {
        { AccidentType.BrokenLeg, [
            "just broke his leg and is grounded!",
            "broke his leg, which is practically all he consists of!",
            "suffered from an open fracture. All he can do now is watch the others win!",
            "broke his only leg and now looks pretty helpless!",
            "tripped over a root and broke his leg!"
        ] },
        { AccidentType.Overheat, [
            "has been running faster than he should have. He burned of overheat!",
            "burned by friction. Needs to cool down a bit before the next race!",
            "roasted on the track from overheat. He's been running way too fast!",
            "looks like he has been running faster than his body cooling system can handle!",
            "shouldn't have been speeding like that. Overheating can be dangerous!"
        ] },
        { AccidentType.HeartAttack, [
            "had a heart attack. Definitely needs a rest!",
            "has a poor heart condition. Hadn't he stopped now, it could have killed him!",
            "beaten by cardiac infarction. He'd better go to hospital asap!",
            "almost killed by heart attack. He had a really narrow escape!",
            "beaten by his weak heart. He'd better get some rest!"
        ] },
        { AccidentType.Grass, [
            "just found magic grass. It's famous for powering slugs up!",
            "just about to speed up after eating magic grass!",
            "powered up by magic grass found unexpectedly on the track!",
            "seems to be full of beans after having eaten the magic grass on his way!",
            "heading perhaps even for victory after his magic grass meal!"
        ] },
        { AccidentType.Asleep, [
            "just fell asleep for a while after the long and wearisome running!",
            "having a nap. He again has chosen just the perfect time for that!",
            "sleeping instead of running. It's getting one of his bad habits!",
            "always takes a short nap at this time of the day, no matter what he's doing!",
            "knows how important sleep is. Even if it's not the best time for that!"
        ] },
        { AccidentType.Blind, [
            "gone blind. Now staggering to find his way!",
            "shouldn't have been reading in dark. Now it's hard to find the way!",
            "temporarily lost his eyesight. Now it's difficult for him to follow the track!",
            "trying hard to find his way after going blind on track!",
            "staggering to finish the race after going blind because of an infection!"
        ] },
        { AccidentType.Puddle, [
            "drowning in a puddle of water!",
            "beaten by yesterday's heavy rainfalls. Just drowning in a puddle!",
            "shouldn't have skipped his swimming lessons. Drowning in a puddle now!",
            "has always neglected his swimming lessons. How wrong he’s been!",
            "disappearing in a puddle of water formed afted heavy rainfall!"
        ] },
        { AccidentType.Electroshock, [
            "speeding up after being struck by lightning!",
            "powered up by lightning. Now running really fast!",
            "hit by electric discharge. Seems to have been powered up by it!",
            "accelerated by a series of electric discharges!",
            "now running much faster after being struck by lightning!"
        ] },
        { AccidentType.TurningBack, [
            "has forgotten to turn off the gas. Must hurry home before it's too late!",
            "just received a phone call. His house is on fire. No time to lose!",
            "seems to have more interesting stuff to do than racing.",
            "seems to have lost orientation. Well, how these little brains work!",
            "has left his snack in the kitchen. He won't race when he's hungry!"
        ] },
        { AccidentType.Devoured, [
            "devoured by the infamous slug monster. Bad luck!",
            "just swallowed by the terrible slug monster!",
            "next on the long list of the slug monster's victims!",
            "has never suspected he's gonna end up as a snack!",
            "devoured by the legendary slug monster from the nearby swamps!"
        ] }
    };

    private readonly Dictionary<AccidentType, string> Sounds = new()
    {
        { AccidentType.BrokenLeg, "Broken Leg.mp3" },
        { AccidentType.Overheat, "Overheat.mp3" },
        { AccidentType.HeartAttack, "Heart Attack.mp3" },
        { AccidentType.Grass, "Grass.mp3" },
        { AccidentType.Asleep, "Asleep.mp3" },
        { AccidentType.Blind, "Blind.mp3" },
        { AccidentType.Puddle, "Drown.mp3" },
        { AccidentType.Electroshock, "Electroshock.mp3" },
        { AccidentType.TurningBack, "Turning Back.mp3" },
        { AccidentType.Devoured, "Devoured.mp3" }
    };

    private readonly Dictionary<AccidentType, uint> AccidentDurations = new()
    {
        { AccidentType.BrokenLeg, 0 },
        { AccidentType.Overheat, 0 },
        { AccidentType.HeartAttack, 0 },
        { AccidentType.Grass, 2000 },
        { AccidentType.Asleep, 0 },
        { AccidentType.Blind, 10000 },
        { AccidentType.Puddle, 0 },
        { AccidentType.Electroshock, 2000 },
        { AccidentType.TurningBack, 0 },
        { AccidentType.Devoured, 0 }
    };

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Name))]
    [NotifyPropertyChangedFor(nameof(Headline))]
    [NotifyPropertyChangedFor(nameof(Sound))]
    private AccidentType accidentType;
 
    public string Name => AccidentNames[AccidentType];

    public string Headline
    {
        get
        {
            var availableHeadlines = Headlines[AccidentType];
            return availableHeadlines[new Random().Next(0, availableHeadlines.Length)];
        }
    }

    public string Sound => Sounds[AccidentType];

    public uint TimePosition
    {
        get => accident.TimePosition;
        set
        {
            if (accident.TimePosition != value)
            {
                accident.TimePosition = value;
                OnPropertyChanged();
            }
        }
    }

    public uint Duration => AccidentDurations[AccidentType];

    [ObservableProperty]
    private SlugViewModel affectedSlug;   

    public bool Expected => new Random().Next(0, 4) == 0;

    public AccidentViewModel(AccidentType accidentType)
    {
        accident = new Accident();
        AccidentType = accidentType;
    }
}
