using Microsoft.AspNetCore.Identity;

namespace LeagueApp.Models;

public class User : IdentityUser
{
    public User()
    {
        FavoriteTeams = new HashSet<Team>();
    }

    public virtual ICollection<Team> FavoriteTeams { get; set; }
}
