namespace LeagueApp.Models;

public class Team
{
    public int TeamId { get; set; }
    public string Name { get; set; }
    public int LeagueId { get; set; }
    public League League { get; set; }
    public TeamStats Stats { get; set; }


    public ICollection<Match> HomeMatches { get; set; }
    public ICollection<Match> AwayMatches { get; set; }
    public ICollection<User> Fans { get; set; }

    public Team()
    {
        HomeMatches = new HashSet<Match>();
        AwayMatches = new HashSet<Match>();
        Fans = new HashSet<User>();
    }
}