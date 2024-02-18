using LeagueApp.Data;
using LeagueApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LeagueApp.Services;

public class TeamStatsService : ITeamStatsService
{
    private readonly ApplicationDbContext _context;

    public TeamStatsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TeamStats> GetTeamStatsByTeamIdAsync(int teamId)
    {
        return await _context.TeamStats.FirstOrDefaultAsync(ts => ts.TeamId == teamId);
    }

    public async Task UpdateTeamStatsAsync(TeamStats teamStats)
    {
        _context.Entry(teamStats).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
