namespace Pin.LiveSports.Core.Dtos
{
    public class Player
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public bool IsPlaying { get; set; }
        public int YellowCardCount { get; set; }
        public int RedCardCount { get; set; }
        public int SubbedInCount { get; set; }
        public int SubbedOutCount { get; set; }
        public int GoalCount { get; set; }
        public bool IsDisqualified { get; set; }
    }
}
