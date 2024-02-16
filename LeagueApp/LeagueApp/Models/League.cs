namespace LeagueApp.Models;

public class League
{
    public int LeagueId { get; set; }
    public string Name { get; set; }
    public ICollection<Team> Teams { get; set; }
    public ICollection<Match> Matches { get; set; }

    public League()
    {
        Teams = new HashSet<Team>();
        Matches = new HashSet<Match>();
    }
}
