namespace Pin.LiveSports.Core.Dtos
{
    public class Team
    {
        public string Name { get; set; }
        public string Crest { get; set; }
        public List<Player> Squad { get; set; }

        private Dictionary<string, int> yellowCardCounts = new Dictionary<string, int>();

        public void IssueYellowCard(string playerName)
        {
            var player = Squad.FirstOrDefault(p => p.Name == playerName);
            if (player == null) return;

            player.YellowCardCount++;

            if (player.YellowCardCount == 2 && !player.IsDisqualified)
            {
                player.IsDisqualified = true;
            }
        }

        public void IssueRedCard(string playerName)
        {
            var player = Squad.FirstOrDefault(p => p.Name == playerName);
            if (player == null) return;

            player.RedCardCount++;
            player.IsDisqualified = true;
        }

        public void PerformSubstitution(string playerIn, string playerOut)
        {
            var playerGoingOut = Squad.FirstOrDefault(p => p.Name == playerOut);
            var playerComingIn = Squad.FirstOrDefault(p => p.Name == playerIn);

            if (playerGoingOut != null && playerComingIn != null)
            {
                playerGoingOut.IsPlaying = false;
                playerComingIn.IsPlaying = true;
            }
        }
    }
}
