namespace LeagueApp.Models;

public class Match
{
    public int MatchId { get; set; }
    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; }
    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; }
    public int LeagueId { get; set; }
    public League League { get; set; }
    public DateTime MatchTime { get; set; }
    public int ScoreHome { get; set; }
    public int ScoreAway { get; set; }
    public bool IsBest { get; set; }
}
