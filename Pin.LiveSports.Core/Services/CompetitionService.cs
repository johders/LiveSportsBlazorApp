using Pin.LiveSports.Core.Dtos;
using Pin.LiveSports.Core.Models;
using Pin.LiveSports.Core.Services.Interfaces;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Pin.LiveSports.Core.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly string _dataFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "world-cup.json");

        public async Task<ResultModel<IEnumerable<Team>>> GetAllTeamsAsync()
        {
            var result = new ResultModel<IEnumerable<Team>>();
            try
            {
                var jsonData = await File.ReadAllTextAsync(_dataFilePath);

                var root = JsonSerializer.Deserialize<Root>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var teams = root?.Teams ?? new List<Team>();

                foreach (var team in teams)
                {
                    foreach (var player in team.Squad)
                    {
                        if (string.IsNullOrWhiteSpace(player.Position))
                        {
                            player.Position = "Midfield";
                        }
                    }
                }

                result.Data = teams;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"An error occurred while fetching teams: {ex.Message}");
                return result;
            }
        }

        public async Task<ResultModel<Competition>> GetCompetitionAsync()
        {
            var result = new ResultModel<Competition>();
            try
            {
                var jsonData = await File.ReadAllTextAsync(_dataFilePath);

                var root = JsonSerializer.Deserialize<Root>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var competition = root?.Competition ?? new Competition();

                result.Data = competition;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"An error occurred while fetching competition: {ex.Message}");
                return result;
            }
        }

        public async Task<ResultModel<Matchup>> GenerateMatchupAsync()
        {
            var result = new ResultModel<Matchup>();
            try
            {
                var allTeamsResult = await GetAllTeamsAsync();
                if (allTeamsResult.Errors.Any() || allTeamsResult.Data == null)
                {
                    result.Errors.Add("Failed to fetch teams.");
                    return result;
                }

                var allTeams = allTeamsResult.Data.ToList();
                if (allTeams.Count < 2)
                {
                    result.Errors.Add("Not enough teams to create a matchup.");
                    return result;
                }

                var random = new Random();
                var firstTeamIndex = random.Next(allTeams.Count);
                Team firstTeam = allTeams[firstTeamIndex];

                Team secondTeam;
                do
                {
                    secondTeam = allTeams[random.Next(allTeams.Count)];
                } while (secondTeam == firstTeam);

                AssignPlayers(firstTeam);
                AssignPlayers(secondTeam);

                var matchup = new Matchup
                {
                    TeamA = firstTeam,
                    TeamB = secondTeam
                };

                result.Data = matchup;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"An error occurred while generating a matchup: {ex.Message}");
                return result;
            }
        }

        private void AssignPlayers(Team team)
        {
            var random = new Random();

            var goalkeepers = team.Squad.Where(player => player.Position.Equals("Goalkeeper", StringComparison.OrdinalIgnoreCase)).ToList();
            var otherPlayers = team.Squad.Where(player => !player.Position.Equals("Goalkeeper", StringComparison.OrdinalIgnoreCase)).ToList();

            if (goalkeepers.Count == 0)
            {
                throw new InvalidOperationException($"Team {team.Name} does not have a goalkeeper.");
            }

            var selectedGoalkeeper = goalkeepers[random.Next(goalkeepers.Count)];
            selectedGoalkeeper.IsPlaying = true;

            var shuffledOtherPlayers = otherPlayers.OrderBy(_ => random.Next()).ToList();

            for (int i = 0; i < shuffledOtherPlayers.Count; i++)
            {
                shuffledOtherPlayers[i].IsPlaying = i < 10;
            }

            foreach (var player in team.Squad)
            {
                if (!player.IsPlaying)
                {
                    player.IsPlaying = false;
                }
            }
        }
    }
}
