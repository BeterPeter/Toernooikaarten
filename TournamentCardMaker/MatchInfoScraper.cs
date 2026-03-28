using System.Web;
using HtmlAgilityPack;

namespace TournamentCardMaker;

public class MatchInfoScraper(string tournamentId, string date)
{
    private string _scheduleHtml = string.Empty;

    public MatchInfoScraper(string scheduleHtml) : this(string.Empty, string.Empty)
    {
        _scheduleHtml = scheduleHtml;
    }

    public async Task<MatchInfo[]> GetMatchesAsync()
    {

        if (string.IsNullOrEmpty(_scheduleHtml))
        {
            var client = HttpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://mijnknltb.toernooi.nl/tournament/{tournamentId}/matches/{date}");
            _scheduleHtml = await response.Content.ReadAsStringAsync();
        }

        var result = new List<MatchInfo>();

        _scheduleHtml.Replace("<meta charset=\"utf-8\">", "<meta charset=\"utf-8\"\\>");
        var doc = new HtmlDocument();
        doc.LoadHtml(_scheduleHtml);
        var matchGroupNodes = doc.DocumentNode.SelectNodes("//div[@class='match-group__wrapper']");
        if (matchGroupNodes == null)
        {
            Console.WriteLine("Found nothing...");
            return [];
        }

        foreach (var matchGroupNode in matchGroupNodes)
        {
            if (matchGroupNode == null)
                continue;

            var time = matchGroupNode
                .SelectSingleNode(".//h5[contains(@class,'match-group__header')]")
                .InnerText
                .Trim();
            var matchNodes = matchGroupNode.SelectNodes(".//div[contains(@class,'match--list')]");
            if (matchNodes == null)
            {
                Console.Write($"Unexpected: no matches at {time}");
                continue;
            }

            foreach (var matchNode in matchNodes)
            {
                var warnings = matchNode.SelectNodes(".//span[contains(@class,'tag--warning')]");
                if (warnings?.Count > 0)
                {
                    Console.WriteLine("Skipping match with warning tag");
                    continue;
                }

                var titleItems = matchNode.SelectNodes(".//li[@class='match__header-title-item']");
                var teams = matchNode
                    .SelectSingleNode(".//div[@class='match__row-wrapper']")
                    .SelectNodes("./div")
                    ?.Select(n => n.SelectNodes(".//div[@class='match__row-title-value']")
                        ?.Select(x => HttpUtility.HtmlDecode(x.InnerText.Trim()))
                        ?.ToArray() ?? [])
                    ?.ToArray();
                var matchInfo = new MatchInfo
                {
                    Time = time ?? string.Empty,
                    Title = titleItems != null && titleItems.Count > 0
                        ? string.Join(", ", titleItems.Select(n => HttpUtility.HtmlDecode(n.InnerText.Trim())))
                        : string.Empty,
                    TopPlayers = teams?.Length > 0 ? teams[0] : [],
                    BottomPlayers = teams?.Length > 1 ? teams[1] : [],
                };
                result.Add(matchInfo);
            }
        }
        
        return [.. result];
    }
    

}