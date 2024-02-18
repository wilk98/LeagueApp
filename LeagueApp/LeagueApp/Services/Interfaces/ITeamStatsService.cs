using LeagueApp.Models;

namespace LeagueApp.Services;

public interface ITeamStatsService
{
    Task<TeamStats> GetTeamStatsByTeamIdAsync(int teamId);
    Task UpdateTeamStatsAsync(TeamStats teamStats);
}
