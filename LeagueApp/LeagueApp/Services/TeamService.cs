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

    public async Task<IEnumerable<Team>> GetAllTeamsAsync()
    {
        return await _context.Teams
        .Include(team => team.Stats)
        .ToListAsync();
    }

    public async Task<Team> GetTeamByIdAsync(int id)
    {
        return await _context.Teams.FindAsync(id);
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
