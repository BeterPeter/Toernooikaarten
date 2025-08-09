namespace TournamentCardMaker;

public class MatchInfo
{
    public string Title { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public string[] TopPlayers { get; set; } = [];
    public string[] BottomPlayers { get; set; } = [];
}