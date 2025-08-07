using System.Web;
using HtmlAgilityPack;

namespace TournamentCardMaker;


public class MatchInfoScraper(string tournamentId, string date)
{
    public async Task<MatchInfo[]> GetMatchesAsync()
    {
        var result = new List<MatchInfo>();
        var client = HttpClientFactory.CreateClient();

        var response = await client.GetAsync($"https://mijnknltb.toernooi.nl/tournament/{tournamentId}/matches/{date}");
        var scheduleHtml = await response.Content.ReadAsStringAsync();
        scheduleHtml.Replace("<meta charset=\"utf-8\">", "<meta charset=\"utf-8\"\\>");
        var doc = new HtmlDocument();
        doc.LoadHtml(scheduleHtml);
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
            var matchNodes = matchGroupNode.SelectNodes(".//div[@class='match match--list']");
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
                var playerItems = matchNode.SelectNodes(".//div[@class='match__row-title-value']");
                var matchInfo = new MatchInfo
                {
                    Time = time ?? string.Empty,
                    Title = titleItems != null && titleItems.Count > 0
                        ? string.Join(", ", titleItems.Select(n => HttpUtility.HtmlDecode(n.InnerText.Trim())))
                        : string.Empty,
                    Players = playerItems
                        ?.Select(x => HttpUtility.HtmlDecode(x.InnerText.Trim()))
                        ?.ToArray() ?? [],
                };
                result.Add(matchInfo);
            }
        }
        
        return [.. result];
    }
    

}