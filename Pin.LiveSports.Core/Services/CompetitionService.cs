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

                result.Data = teams;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"An error occurred while fetching teams: {ex.Message}");
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

            var shuffledSquad = team.Squad.OrderBy(_ => random.Next()).ToList();

            for (int i = 0; i < shuffledSquad.Count; i++)
            {
                shuffledSquad[i].IsPlaying = i < 11;
            }
        }
    }
}
