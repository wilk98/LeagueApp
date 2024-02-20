using LeagueApp.Data;
using LeagueApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LeagueApp.Services;

public class TeamService : ITeamService
{
    private readonly ApplicationDbContext _context;

    public TeamService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Team>> GetAllTeamsAsync(int leagueId)
    {
        return await _context.Teams
            .Where(team => team.LeagueId == leagueId)
            .Include(team => team.Stats)
            .Include(team => team.HomeMatches)
            .Include(team => team.AwayMatches)
            .ToListAsync();
    }


    public async Task<Team> GetTeamByIdAsync(int id)
    {
        return await _context.Teams
            .Include(team => team.Stats)
            .Include(team => team.HomeMatches)
            .Include(team => team.AwayMatches)
            .FirstOrDefaultAsync(team => team.TeamId == id);
    }


    public async Task<Team> CreateTeamAsync(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }

    public async Task UpdateTeamAsync(Team team)
    {
        _context.Entry(team).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTeamAsync(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team != null)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }
    }
}
