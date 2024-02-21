using LeagueApp.Models;
using Microsoft.AspNetCore.Identity;

namespace LeagueApp.Services.Interfaces;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
    Task<SignInResult> LoginUserAsync(LoginViewModel model);
    Task LogoutUserAsync();
    Task<IdentityResult> AddTeamToFavoritesAsync(string userId, int teamId);
    Task<IdentityResult> RemoveTeamFromFavoritesAsync(string userId, int teamId);
    Task<List<int>> GetUserFavoriteTeamIdsAsync(string userId);
}

