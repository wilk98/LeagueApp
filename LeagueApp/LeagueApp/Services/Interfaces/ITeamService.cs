using LeagueApp.Models;

namespace LeagueApp.Services;

public interface ITeamService
{
    Task<IEnumerable<Team>> GetAllTeamsAsync(int leagueId);
    Task<Team> GetTeamByIdAsync(int id);
    Task<Team> CreateTeamAsync(Team team);
    Task UpdateTeamAsync(Team team);
    Task DeleteTeamAsync(int id);
}
