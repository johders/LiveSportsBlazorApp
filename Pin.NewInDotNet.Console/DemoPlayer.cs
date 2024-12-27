public class DemoPlayer(string Name = "Franky Van der Elst", string Position = "Defence", bool IsPlaying = true) 
{
    public override string ToString()
    {
        return $"{Name}  - {Position} - Is Playing: {IsPlaying}";
    }
}

