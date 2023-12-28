namespace Slugrace.Models;

public enum AccidentType
{
    BrokenLeg,
    Overheat,
    HeartAttack,
    Grass,
    Asleep,
    Blind,
    Puddle,
    Electroshock,
    TurningBack,
    Devoured
}

class Accident
{
    public AccidentType Type { get; set; }
    public string Name { get; set; }
    public string Headline { get; set; }
    public string Sound { get; set; }
    public uint TimePosition { get; set; }
}
