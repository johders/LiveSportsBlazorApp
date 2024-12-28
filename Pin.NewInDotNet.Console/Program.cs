using Pin.LiveSports.Core.Services;
using Pin.NewInDotNet.Console;

// Demonstrating C# 12 Features: Primary Constructors, Collection Expressions, and Default Lambda Expressions

public delegate int AddGoalsDelegate(DemoTeam team, int goalCount = 1);

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Let's first fetch some mock data
        var competitionService = new CompetitionService();
        var allTeams = (await competitionService.GetAllTeamsAsync()).Data;

        // Map teams to `DemoTeam` objects with nested `DemoPlayer` objects
        var demoTeams = allTeams.Select(t => new DemoTeam
        {
            Name = t.Name,
            Crest = t.Crest,
            Players = t.Squad.Select(s => new DemoPlayer(s.Name, s.Position, s.IsPlaying)).ToList()
        });

        //Now let's get the first team from the list so we have some players to work with
        var firstTeamPlayers = demoTeams.First().Players;

        // 1. Primary Constructors (and a taste of Collection Expressions)
        Console.WriteLine("### Primary Constructors and Collection Expressions ###");

        //Since we set up the DemoPlayer records with a primary constructor, we can just add new players to our team of Frankies
        //Two things to note here: Firstly, we are using a collection expression to populate our list of demoplayer objects. This is done with the [] notation.
        //Secondly, thanks to having set a primary constructor for the demoplayer objects with default values, we can quickly instantiate new DemoPlayers with default values
        //just by instantiating "new DemoPlayer()"

        List<DemoPlayer> lotsOfFrankies = [
            new DemoPlayer(),
            new DemoPlayer(),
            new DemoPlayer(),
            new DemoPlayer(),
            new DemoPlayer(),
            new DemoPlayer(),
            new DemoPlayer()
            ];

        Console.WriteLine("Lots of Frankies:");
        lotsOfFrankies.ForEach(Console.WriteLine);

        //Of course we are not limited to this and can still assign custom values to the properties:
        List<DemoPlayer> otherPlayers = new List<DemoPlayer>();
        otherPlayers.Add(new DemoPlayer("Jefke Delaplace", "Offence", false));
        otherPlayers.Add(new DemoPlayer("Jean-Marie Pfaff", "Goalkeeper", false));
        otherPlayers.Add(new DemoPlayer(Position: "Centre-Forward"));
        otherPlayers.Add(new DemoPlayer(IsPlaying: false));

        Console.WriteLine("\nCustom Players:");
        otherPlayers.ForEach(Console.WriteLine);

        //I can definitely see its strengths in tasks like seeding data for any database throughout our course or just wanting to be able to quickly create object instances
        //for small scale application of just trying things out.

        //2. Collection Expressions with spread operator
        Console.WriteLine("\n### Collection Expressions with Spread Operator ###");

        //Using the spread operator, we can make our first team even better by adding a collection of Frankies and other custom players.
        //Note here that thanks to the spread operator ".. collection", we no longer need to loop through each collection in order to add them to the new list.

        //The spread operator would come in very useful when having to combine different collections into one. I remember our Train Station PE in semester 2 where I combined a list
        //of delayed trains and departed trains in order to make the required announcements on the board. Many loops could have been avoided using this new C# 12 feature!
        DemoTeam newTeam = new DemoTeam { Name = "Demo Team" };
        List<DemoPlayer> newSquad = [.. firstTeamPlayers, .. lotsOfFrankies, .. otherPlayers];

        newTeam.Players = newSquad;

        Console.WriteLine("New Team:");
        newSquad.ForEach(Console.WriteLine);

        //3. Default Lambda Expressions
        Console.WriteLine("\n### Default Lambda Expressions ###");

        // Just as using default parameter values in methods, we can now use them in lambda expressions. I can see how this can simplify code in certain scenarios.
        // I played around with a simple delegate to add one to the team's score when only the team is provided (default increment value being set to 1)
        // This default increment value can be overridden by providing a second argument.
        // Besides that, it could also be useful for simplifying repeated LINQ queries as demonstrated in the demo code below.
        // After reading through the documentation, I understand this feature coming in handy for plenty other more advanced scenarios and
        // while my current experience with lambda expressions is fairly limited, I am looking forward to exploring these further in the future


        AddGoalsDelegate AddGoalsScored = (DemoTeam team, int goalCount = 1) =>
        {
            team.GoalsScored += goalCount;
            return team.GoalsScored;
        };

        Console.WriteLine($"{newTeam.Name} scored! This season's goal count is now: {AddGoalsScored(newTeam)}");
        Console.WriteLine($"{newTeam.Name} scored! This season's goal count is now: {AddGoalsScored(newTeam, 12)}");

        var FilterPlayersByPosition = (List<DemoPlayer> list, string position = "Offence") =>
        {
            return list.Where(p => p.Position.Contains(position));
        };

        Console.WriteLine("\nFiltered by position Offence (default)");
        var offence = FilterPlayersByPosition(newSquad).ToList();
        offence.ForEach(Console.WriteLine);

        Console.WriteLine("\nFiltered by position Defence");
        var defence = FilterPlayersByPosition(newSquad, "Defence").ToList();
        defence.ForEach(Console.WriteLine);
    }
}

// Supporting Classes
namespace Pin.NewInDotNet.Console
{
    public class DemoTeam
    {
        public string Name { get; set; }
        public string Crest { get; set; }
        public List<DemoPlayer> Players { get; set; } = new();
        public int GoalsScored { get; set; }
    }
}

public record DemoPlayer(string Name = "Franky Van der Elst", string Position = "Defence", bool IsPlaying = true)
{
    public override string ToString()
        => $"{Name} - {Position} - Is Playing: {IsPlaying}";
}

