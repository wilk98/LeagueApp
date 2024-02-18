using LeagueApp.Data;
using LeagueApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LeagueApp.Services;

public class LeagueService : ILeagueService
{
    private readonly ApplicationDbContext _context;

    public LeagueService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<League>> GetAllLeaguesAsync()
    {
        return await _context.Leagues.ToListAsync();
    }

    public async Task<League> GetLeagueByIdAsync(int id)
    {
        return await _context.Leagues.FindAsync(id);
    }

    public async Task<League> CreateLeagueAsync(League league)
    {
        _context.Leagues.Add(league);
        await _context.SaveChangesAsync();
        return league;
    }

    public async Task UpdateLeagueAsync(League league)
    {
        _context.Entry(league).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLeagueAsync(int id)
    {
        var league = await _context.Leagues.FindAsync(id);
        if (league != null)
        {
            _context.Leagues.Remove(league);
            await _context.SaveChangesAsync();
        }
    }
}
