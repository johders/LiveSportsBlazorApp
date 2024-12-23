namespace Pin.LiveSports.Core.Dtos
{
    public class Player
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public bool IsPlaying { get; set; }
        public int YellowCardCount { get; set; }
        public int RedCardCount { get; set; }
        public bool IsDisqualified { get; set; }
    }
}
