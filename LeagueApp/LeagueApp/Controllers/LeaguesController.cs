using Microsoft.AspNetCore.Mvc;
using LeagueApp.Services;
using LeagueApp.Models;

namespace LeagueApp.Controllers;

public class LeaguesController : Controller
{
    private readonly ILeagueService _leagueService;

    public LeaguesController(ILeagueService leagueService)
    {
        _leagueService = leagueService;
    }

    public async Task<IActionResult> Index()
    {
        var leagues = await _leagueService.GetAllLeaguesAsync();
        return View(leagues);
    }

    public async Task<IActionResult> Details(int id)
    {
        var league = await _leagueService.GetLeagueByIdAsync(id);
        if (league == null)
        {
            return NotFound();
        }
        return View(league);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("LeagueId,Name")] League league)
    {
        if (ModelState.IsValid)
        {
            await _leagueService.CreateLeagueAsync(league);
            return RedirectToAction(nameof(Index));
        }
        return View(league);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var league = await _leagueService.GetLeagueByIdAsync(id);
        if (league == null)
        {
            return NotFound();
        }
        return View(league);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("LeagueId,Name")] League league)
    {
        if (id != league.LeagueId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _leagueService.UpdateLeagueAsync(league);
            return RedirectToAction(nameof(Index));
        }
        return View(league);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var league = await _leagueService.GetLeagueByIdAsync(id);
        if (league == null)
        {
            return NotFound();
        }
        return View(league);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _leagueService.DeleteLeagueAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
