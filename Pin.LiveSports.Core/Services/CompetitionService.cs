using Pin.LiveSports.Core.Dtos;
using Pin.LiveSports.Core.Models;
using Pin.LiveSports.Core.Services.Interfaces;
using System.Text.Json;

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

                // Deserialize the JSON into the Root object
                var root = JsonSerializer.Deserialize<Root>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Extract the Teams list
                var teams = root?.Teams ?? new List<Team>();

                // Return a successful result
                result.Data = teams;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"An error occurred while fetching teams: {ex.Message}");
                return result;
            }
        }
    }
}
