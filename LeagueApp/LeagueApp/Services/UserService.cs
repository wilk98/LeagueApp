using LeagueApp.Data;
using LeagueApp.Models;
using LeagueApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeagueApp.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
    {
        var user = new User { UserName = model.Email, Email = model.Email };
        return await _userManager.CreateAsync(user, model.Password);
    }

    public async Task<SignInResult> LoginUserAsync(LoginViewModel model)
    {
        return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
    }

    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> AddTeamToFavoritesAsync(string userId, int teamId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var team = await _context.Teams.FindAsync(teamId);

        if (user == null || team == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Nie znaleziono użytkownika lub drużyny." });
        }

        if (!user.FavoriteTeams.Any(t => t.TeamId == teamId))
        {
            user.FavoriteTeams.Add(team);
            try
            {
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Wystąpił błąd: {ex.Message}" });
            }
        }

        return IdentityResult.Failed(new IdentityError { Description = "Drużyna jest już dodana do ulubionych." });
    }

    public async Task<IdentityResult> RemoveTeamFromFavoritesAsync(string userId, int teamId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var team = await _context.Teams.FindAsync(teamId);

        if (user == null || team == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Nie znaleziono użytkownika lub drużyny." });
        }

        var existingLink = user.FavoriteTeams.FirstOrDefault(t => t.TeamId == teamId);
        if (existingLink != null)
        {
            user.FavoriteTeams.Remove(existingLink);
            try
            {
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Wystąpił błąd: {ex.Message}" });
            }
        }

        return IdentityResult.Failed(new IdentityError { Description = "Drużyna nie została znaleziona w ulubionych." });
    }

    public async Task<List<int>> GetUserFavoriteTeamIdsAsync(string userId)
    {
        var userWithFavorites = await _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.FavoriteTeams)
            .FirstOrDefaultAsync();

        if (userWithFavorites != null)
        {
            return userWithFavorites.FavoriteTeams.Select(t => t.TeamId).ToList();
        }
        return new List<int>();
    }

}
