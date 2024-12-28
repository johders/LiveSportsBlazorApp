using Pin.LiveSports.Core.Services;
using Pin.NewInDotNet.Console;

//To demonstrate the new C# 12 features, let's start by using some mock data from the core project.
//I'll map the teams to the DemoTeam with DemoPlayer objects inside this projects

var competitionService = new CompetitionService();
var allTeams = (await competitionService.GetAllTeamsAsync()).Data;

var demoTeams = allTeams.Select(t => new DemoTeam
{
    Name = t.Name,
    Crest = t.Crest,
    Players = t.Squad.Select(s => new DemoPlayer(s.Name, s.Position, s.IsPlaying)).ToList()
});

//Now let's get the first team from the list so we have some players to work with
var firstTeamPlayers = demoTeams.First().Players;

//Since we set up the DemoPlayer records with a primary constructor, we can just add new players to our team of Frankies
List<DemoPlayer> lotsOfFrankies = [new DemoPlayer(), new DemoPlayer(), new DemoPlayer(), new DemoPlayer(), new DemoPlayer(), new DemoPlayer(), new DemoPlayer()];

List<DemoPlayer> otherPlayers = new List<DemoPlayer>();
otherPlayers.Add(new DemoPlayer("Jefke Delaplace", "Offence", false));
otherPlayers.Add(new DemoPlayer("Jean-Marie Pfaff", "Goalkeeper", false));
otherPlayers.Add(new DemoPlayer(Position: "Centre-Forward"));
otherPlayers.Add(new DemoPlayer(IsPlaying: false));

//Using the spread operator, we can make our first team even better by adding a collection of Frankies and other players
List<DemoPlayer> newTeam = [.. firstTeamPlayers, .. lotsOfFrankies, .. otherPlayers];

foreach (var player in newTeam)
{
    Console.WriteLine(player.ToString());
}
